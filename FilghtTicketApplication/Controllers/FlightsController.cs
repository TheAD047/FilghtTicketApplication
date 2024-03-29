﻿using System;
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
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Flight.Include(f => f.airline);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return View("404");
            }

            var flight = await _context.Flight
                .Include(f => f.airline)
                .FirstOrDefaultAsync(m => m.flightID == id);
            if (flight == null)
            {
                return View("404"); ;
            }

            return View("Details", flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["airlineID"] = new SelectList(_context.Set<Airline>(), "airlineID", "airlineName");
            return View("Create");
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("flightID,flightName,airlineID,departureDate,landingTime,departureFrom,arrivalAt")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["airlineID"] = new SelectList(_context.Set<Airline>(), "airlineID", "airlineName", flight.airlineID);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return View("404");
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight == null)
            {
                return View("404");
            }
            ViewData["airlineID"] = new SelectList(_context.Set<Airline>(), "airlineID", "airlineName", flight.airlineID);
            return View("Edit", flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("flightID,flightName,airlineID,departureDate,landingTime,departureFrom,arrivalAt")] Flight flight)
        {
            if (id != flight.flightID)
            {
                return View("404");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.flightID))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["airlineID"] = new SelectList(_context.Set<Airline>(), "airlineID", "airlineEmail", flight.airlineID);
            return View("Edit", flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .Include(f => f.airline)
                .FirstOrDefaultAsync(m => m.flightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Flight == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Flight'  is null.");
            }
            var flight = await _context.Flight.FindAsync(id);
            if (flight != null)
            {
                _context.Flight.Remove(flight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int? id)
        {
          return _context.Flight.Any(e => e.flightID == id);
        }
    }
}
