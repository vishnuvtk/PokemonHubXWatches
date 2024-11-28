using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .ToListAsync();

            var reservationViewModels = reservations.Select(r => new ReservationViewModel
            {
                ReservationID = r.ReservationID,
                ReservationDate = r.ReservationDate,
                UserId = r.UserId,
                WatchID = r.WatchID,
                UserFullName = r.User.UserName,
                WatchName = r.Watch.Name
            }).ToList();

            return View(reservationViewModels);
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .FirstOrDefaultAsync(r => r.ReservationID == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationViewModel = new ReservationViewModel
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                UserId = reservation.UserId,
                WatchID = reservation.WatchID,
                UserFullName = reservation.User.UserName,
                WatchName = reservation.Watch.Name
            };

            return View(reservationViewModel);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_context.Users, "UserId", "FullName");
            ViewData["Watches"] = new SelectList(_context.Watches, "WatchID", "Name");
            return View();
        }

        // POST: Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservationViewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    ReservationDate = reservationViewModel.ReservationDate,
                    UserId = reservationViewModel.UserId,
                    WatchID = reservationViewModel.WatchID
                };

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Users"] = new SelectList(_context.Users, "UserId", "FullName", reservationViewModel.UserId);
            ViewData["Watches"] = new SelectList(_context.Watches, "WatchID", "Name", reservationViewModel.WatchID);
            return View(reservationViewModel);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationViewModel = new ReservationViewModel
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                UserId = reservation.UserId,
                WatchID = reservation.WatchID,
                UserFullName = reservation.User.UserName,
                WatchName = reservation.Watch.Name
            };

            return View(reservationViewModel);
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservationViewModel reservationViewModel)
        {
            if (id != reservationViewModel.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var reservation = new Reservation
                    {
                        ReservationID = reservationViewModel.ReservationID,
                        ReservationDate = reservationViewModel.ReservationDate,
                        UserId = reservationViewModel.UserId,
                        WatchID = reservationViewModel.WatchID
                    };

                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservationViewModel.ReservationID))
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

            ViewData["Users"] = new SelectList(_context.Users, "UserId", "FullName", reservationViewModel.UserId);
            ViewData["Watches"] = new SelectList(_context.Watches, "WatchID", "Name", reservationViewModel.WatchID);
            return View(reservationViewModel);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Watch)
                .FirstOrDefaultAsync(r => r.ReservationID == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationViewModel = new ReservationViewModel
            {
                ReservationID = reservation.ReservationID,
                ReservationDate = reservation.ReservationDate,
                UserFullName = reservation.User.UserName,
                WatchName = reservation.Watch.Name
            };

            return View(reservationViewModel);
        }

        // POST: Reservation/Delete/5
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
