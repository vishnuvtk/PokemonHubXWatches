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

        // GET: /HeldItem
        public IActionResult Index()
        {
            var heldItems = _heldItemService.GetAllHeldItems();
            return View(heldItems); // Renders the Index view with Held Item list
        }

        // GET: /HeldItem/Details/{id}
        public IActionResult Details(int id)
        {
            var heldItem = _heldItemService.GetHeldItemById(id);
            if (heldItem == null) return NotFound();
            return View(heldItem); // Renders the Details view for a specific Held Item
        }

        // GET: /HeldItem/Create
        public IActionResult Create()
        {
            return View(); // Renders the Create form
        }

        // POST: /HeldItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HeldItemDTO heldItem)
        {
            if (ModelState.IsValid)
            {
                _heldItemService.AddHeldItem(heldItem);
                return RedirectToAction(nameof(Index)); // Redirect to Held Item list
            }
            return View(heldItem); // Return to Create form with validation errors
        }

        // GET: /HeldItem/Edit/{id}
        public IActionResult Edit(int id)
        {
            var heldItem = _heldItemService.GetHeldItemById(id);
            if (heldItem == null) return NotFound();
            return View(heldItem); // Renders the Edit form
        }

        // POST: /HeldItem/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HeldItemDTO heldItem)
        {
            if (ModelState.IsValid)
            {
                var success = _heldItemService.UpdateHeldItem(heldItem);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index)); // Redirect to Held Item list
            }
            return View(heldItem); // Return to Edit form with validation errors
        }

        // GET: /HeldItem/Delete/{id}
        public IActionResult Delete(int id)
        {
            var heldItem = _heldItemService.GetHeldItemById(id);
            if (heldItem == null) return NotFound();
            return View(heldItem); // Renders the Delete confirmation view
        }

        // POST: /HeldItem/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var success = _heldItemService.DeleteHeldItem(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index)); // Redirect to Held Item list
        }
    }
}
