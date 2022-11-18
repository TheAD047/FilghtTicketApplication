using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FilghtTicketApplication.Data;
using FilghtTicketApplication.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FilghtTicketApplication.Controllers
{
    [Authorize(Roles = ("ADMIN"))]
    public class SeatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private String[] rows = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                                 "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V" ,
                                 "W", "X", "Y", "Z"};

        public SeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Seat.Include(s => s.Flight).Include(s => s.Flight.airline);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seat == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat
                .Include(s => s.Flight)
                .Include(s => s.Flight.airline)
                .FirstOrDefaultAsync(m => m.seatID == id);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // GET: Seats/Create
        public IActionResult Create()
        {
            ViewData["flightID"] = new SelectList(_context.Flight, "flightID", "flightName");
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("seatID,flightID,seatNum,seatRow,isBooked")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["flightID"] = new SelectList(_context.Flight, "flightID", "flightName", seat.flightID);
            return View(seat);
        }

        public IActionResult GenerateSeats()
        {
            ViewData["flightID"] = new SelectList(_context.Flight, "flightID", "flightName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateSeats(int flightID, int numOfRows, int numOfSeatsInArow)
        {
            var list = _context.Flight.Where(f => f.flightID == flightID);
            if (list.Any() && numOfRows > 0 && numOfRows <= 26 && numOfSeatsInArow > 0)
            {
                for (int i = 0; i < numOfRows; i++)
                {
                    for (int j = 0; j < numOfSeatsInArow; j++)
                    {
                        Seat seat = new Seat();
                        seat.flightID = flightID;
                        seat.seatNum = j + 1;
                        seat.seatRow = rows[i];
                        seat.isBooked = false;

                        _context.Add(seat);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                return GenerateSeats();
            }
        }

        // GET: Seats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seat == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            ViewData["flightID"] = new SelectList(_context.Flight, "flightID", "flightName", seat.flightID);
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("seatID,flightID,seatNum,seatRow,isBooked")] Seat seat)
        {
            if (id != seat.seatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatExists(seat.seatID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["flightID"] = new SelectList(_context.Flight, "flightID", "flightName", seat.flightID);
            return View(seat);
        }

        // GET: Seats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seat == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat
                .Include(s => s.Flight)
                .FirstOrDefaultAsync(m => m.seatID == id);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seat == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Seat'  is null.");
            }
            var seat = await _context.Seat.FindAsync(id);
            if (seat != null)
            {
                _context.Seat.Remove(seat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeatExists(int id)
        {
          return _context.Seat.Any(e => e.seatID == id);
        }
    }
}
