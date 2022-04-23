using Concert.Data.Entities;

namespace Concert.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTicketsAsync();
            await CheckEntrancesAsync();
        }

        private async Task CheckEntrancesAsync()
        {
            List<string> auxEntrance = new List<string>()
            {
                "Norte",
                "Sur",
                "Oriental",
                "Occidental"
            };
            if (!_context.Entrances.Any())
            {
                for (int i = 0; i < 4; i++)
                {
                    _context.Entrances.Add(new Entrance
                    {
                        Description = auxEntrance[i],
                    });
                    await _context.SaveChangesAsync();
                }

            }
        }

        private async Task CheckTicketsAsync()
        {


            if (!_context.Tickets.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    _context.Tickets.Add(new Ticket
                    {
                        WasUsed = false,
                        Document = "10" + i.ToString(),
                        Name = "XXX"
                    });
                    await _context.SaveChangesAsync();
                }

            }

        }
    }
}
