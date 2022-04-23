using Concert.Data;
using Concert.Data.Entities;
using Concert.Helpers;
using Concert.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        public TicketsController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.
               Include(e => e.TicketEntrance).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            TicketViewModel model = new()
            {
                Entrances = await _combosHelper.GetComboEntranceAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = new()
                {
                    Name = model.Name,
                    Document = model.Document,
                    Date = DateTime.Now,
                    WasUsed = true
                };
                ticket.TicketEntrance = new List<TicketEntrance>()
                {
                    new TicketEntrance
                    {
                        Entrance = await _context.Entrances.FindAsync(model.EntranceId),
                    }
                };

                try
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un ticket con el mismo Id de cliente.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            model.Entrances = await _combosHelper.GetComboEntranceAsync();
            return View(model);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            EditTicketViewModel model = new()
            {
                Id = ticket.Id,
                Name = ticket.Name,
                Document = ticket.Document,
                WasUsed = true,
                Date = DateTime.Now,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Ticket ticket = await _context.Tickets.FindAsync(model.Id);
                ticket.Name = model.Name;
                ticket.Id = model.Id;
                ticket.Document = model.Document;
                ticket.WasUsed = true;
                ticket.Date = DateTime.Now;
                _context.Update(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un boleto con el mismo ID.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(model);
        }

        public async Task<IActionResult> ConsultIdTicket(int? id)
        {
            Console.WriteLine(id);
            return View();
        }
    }
}
