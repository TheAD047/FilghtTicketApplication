using FilghtTicketApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilghtTicketApplication.Controllers
{
    public class ClientViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Flight.Include(f => f.airline);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Seats(int? ID)
        {
            var applicationDbContext = _context.Seat
                                               .OrderBy(s => s.seatRow)
                                               .OrderBy(s => s.seatNum)
                                               .Include(s => s.Flight)
                                               .Include(s => s.Flight.airline)
                                               .Where(s => s.flightID == ID);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
