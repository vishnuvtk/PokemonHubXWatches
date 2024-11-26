using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    public class HeldItemController : Controller
    {
        private readonly IHeldItemService _heldItemService;

        public HeldItemController(IHeldItemService heldItemService)
        {
            _heldItemService = heldItemService;
        }

        // GET: HeldItem
        public async Task<IActionResult> Index()
        {
            var heldItems = await _heldItemService.ListHeldItems();
            return View(heldItems);
        }

        // GET: HeldItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var heldItem = await _heldItemService.FindHeldItem(id);
            if (heldItem == null) return NotFound();

            return View(heldItem);
        }

        // GET: HeldItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HeldItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeldItemName,HeldItemHP,HeldItemAttack,HeldItemDefense,HeldItemSpAttack,HeldItemSpDefense,HeldItemCDR,HeldItemImage")] HeldItem heldItem)
        {
            if (ModelState.IsValid)
            {
                await _heldItemService.CreateHeldItem(heldItem);
                return RedirectToAction(nameof(Index));
            }
            return View(heldItem);
        }

        // GET: HeldItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var heldItem = await _heldItemService.FindHeldItem(id);
            if (heldItem == null) return NotFound();

            return View(heldItem);
        }

        // POST: HeldItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HeldItemId,HeldItemName,HeldItemHP,HeldItemAttack,HeldItemDefense,HeldItemSpAttack,HeldItemSpDefense,HeldItemCDR,HeldItemImage")] HeldItem heldItem)
        {
            if (id != heldItem.HeldItemId) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _heldItemService.UpdateHeldItem(id, heldItem);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(heldItem);
        }

        // GET: HeldItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var heldItem = await _heldItemService.FindHeldItem(id);
            if (heldItem == null) return NotFound();

            return View(heldItem);
        }

        // POST: HeldItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _heldItemService.DeleteHeldItem(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
