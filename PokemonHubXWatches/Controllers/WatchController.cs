using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

namespace PokemonHubXWatches.Controllers
{
    public class WatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Watch
        public async Task<IActionResult> Index()
        {
            var watches = await _context.Watches.ToListAsync();
            var watchViewModels = watches.Select(w => new WatchViewModel
            {
                WatchID = w.WatchID,
                Name = w.Name,
                Price = w.Price,
                Description = w.Description
            }).ToList();

            return View(watchViewModels);
        }

        // GET: Watch/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            var watchViewModel = new WatchViewModel
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Price = watch.Price,
                Description = watch.Description
            };

            return View(watchViewModel);
        }

        // GET: Watch/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Watch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WatchViewModel watchViewModel)
        {
            if (ModelState.IsValid)
            {
                var watch = new Watch
                {
                    Name = watchViewModel.Name,
                    Price = watchViewModel.Price,
                    Description = watchViewModel.Description
                };

                _context.Add(watch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watchViewModel);
        }

        // GET: Watch/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            var watchViewModel = new WatchViewModel
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Price = watch.Price,
                Description = watch.Description
            };

            return View(watchViewModel);
        }

        // POST: Watch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WatchViewModel watchViewModel)
        {
            if (id != watchViewModel.WatchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var watch = new Watch
                    {
                        WatchID = watchViewModel.WatchID,
                        Name = watchViewModel.Name,
                        Price = watchViewModel.Price,
                        Description = watchViewModel.Description
                    };

                    _context.Update(watch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchExists(watchViewModel.WatchID))
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
            return View(watchViewModel);
        }

        // GET: Watch/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            var watchViewModel = new WatchViewModel
            {
                WatchID = watch.WatchID,
                Name = watch.Name,
                Price = watch.Price,
                Description = watch.Description
            };

            return View(watchViewModel);
        }

        // POST: Watch/Delete/5
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

        private bool WatchExists(int id)
        {
            return _context.Watches.Any(e => e.WatchID == id);
        }
    }
}
