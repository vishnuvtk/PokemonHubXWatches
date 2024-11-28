using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class WatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Watches
        public async Task<IActionResult> Index()
        {
            var watches = await _context.Watches.ToListAsync();
            return View(watches);
        }

        // GET: Watches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Watches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WatchID,Name,Price,Description")] Watch watch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watch);
        }

        // GET: Watches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watches.FindAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            var watchDTO = new WatchDTO
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Price = watch.Price,
                Description = watch.Description
            };

            return View(watchDTO);
        }

        // POST: Watches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WatchID,Name,Price,Description")] WatchDTO watchDTO)
        {
            if (id != watchDTO.WatchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var watch = new Watch
                    {
                        WatchID = watchDTO.WatchID,
                        Name = watchDTO.Name,
                        Price = watchDTO.Price,
                        Description = watchDTO.Description
                    };

                    _context.Update(watch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchExists(watchDTO.WatchID))
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
            return View(watchDTO);
        }

        // GET: Watches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watches.FirstOrDefaultAsync(m => m.WatchID == id);
            if (watch == null)
            {
                return NotFound();
            }

            // Create WatchDTO for the view
            var watchDTO = new WatchDTO
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Price = watch.Price,
                Description = watch.Description
            };

            return View(watchDTO);
        }

        // POST: Watches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch != null)
            {
                _context.Watches.Remove(watch);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Watches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watches.FirstOrDefaultAsync(m => m.WatchID == id);
            if (watch == null)
            {
                return NotFound();
            }

            var watchDTO = new WatchDTO
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Price = watch.Price,
                Description = watch.Description
            };

            return View(watchDTO);
        }

        private bool WatchExists(int id)
        {
            return _context.Watches.Any(e => e.WatchID == id);
        }
    }
}
