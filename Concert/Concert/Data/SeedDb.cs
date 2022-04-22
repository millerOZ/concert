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
        }

        private async Task CheckTicketsAsync()
        {

            List<string> auxEntrance = new List<string>()
            {
                "Norte",
                "Sur",
                "Oriental",
                "Occidental"
            };
            if (!_context.Tickets.Any())
            {
                //var seed = Environment.TickCount;
                //var random = new Random(seed);
                //for (int i = 0; i < 10; i++)
                //{
                //    _context.Tickets.Add(new Ticket
                //    {
                //        WasUsed = false,
                //        Entrances = new List<Entrance>()
                //    {
                //        new Entrance
                //        {
                //            Description = auxEntrance[random.Next(0, 4)],
                //        }
                //    }
                //    });
                //}
                _context.Tickets.Add(new Ticket
                {
                    WasUsed = false,
                    Entrances = new List<Entrance>()
                    {
                        new Entrance
                        {
                            Description = "Norte",
                        }
                    }
                });
            }

        }
    }
}
