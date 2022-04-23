using Concert.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Concert.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboEntranceAsync()
        {
            List<SelectListItem> list = await _context.Entrances.Select(c => new SelectListItem
            {
                Text = c.Description,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text)
              .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;
        }
    }
}
