using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .Select(r => new ReservationDTO
                {
                    ReservationID = r.ReservationID,
                    ReservationDate = r.ReservationDate,
                    UserID = r.UserId,
                    UserFullName = r.User.UserName, // Include the full name of the user
                    WatchID = r.WatchID,
                    WatchName = r.Watch.Name // Include the name of the watch
                })
                .ToListAsync();

            return View(reservations);
        }


        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Watch)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDTO = new ReservationDTO
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                WatchID = reservation.WatchID,
                UserID = reservation.UserId,
                WatchName = reservation.Watch.Name,
                UserFullName = reservation.User.UserName
            };

            return View(reservationDTO);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            // Fetch all watches
            var allWatches = _context.Watches.ToList();

            // Get the list of reserved watch IDs
            var reservedWatchIds = _context.Reservations.Select(r => r.WatchID).ToList();

            // Filter out the watches that are already reserved
            var availableWatches = allWatches.Where(watch => !reservedWatchIds.Contains(watch.WatchID)).ToList();

            // Pass the filtered watches to the view
            ViewData["Watches"] = availableWatches;

            // Pass the users to the view
            ViewData["Users"] = _context.Users.ToList();

            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationDate,UserID,WatchID")] ReservationDTO reservationDTO)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    ReservationDate = reservationDTO.ReservationDate,
                    UserId = reservationDTO.UserID,
                    WatchID = reservationDTO.WatchID
                };

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Users"] = new SelectList(_context.Users, "UserID", "FullName", reservationDTO.UserID);
            ViewData["Watches"] = new SelectList(_context.Watches, "WatchID", "Name", reservationDTO.WatchID);
            return View(reservationDTO);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var allWatches = _context.Watches.ToList();

            // Get the list of reserved watch IDs
            var reservedWatchIds = _context.Reservations.Select(r => r.WatchID).ToList();

            // Filter out the watches that are already reserved
            var availableWatches = allWatches.Where(watch => !reservedWatchIds.Contains(watch.WatchID)).ToList();

            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDTO = new ReservationDTO
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                UserID = reservation.UserId,
                WatchID = reservation.WatchID
            };

            // Populate the ViewData with users and available watches
            ViewData["Users"] = _context.Users.ToList();
            ViewData["Watches"] = _context.Watches
                .Where(w => !_context.Reservations.Select(r => r.WatchID).Contains(w.WatchID) || w.WatchID == reservation.WatchID)
                .ToList();

            return View(reservationDTO);
        }


        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Watch)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDTO = new ReservationDTO
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                WatchID = reservation.WatchID,
                UserID = reservation.UserId,
                WatchName = reservation.Watch.Name,
                UserFullName = reservation.User.UserName
            };

            return View(reservationDTO);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}

