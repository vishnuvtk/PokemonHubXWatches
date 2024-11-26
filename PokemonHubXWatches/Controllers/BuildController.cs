using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    public class BuildController : Controller
    {
        private readonly IBuildService _buildService;
        private readonly IPokemonService _pokemonService;
        private readonly IHeldItemService _heldItemService;

        public BuildController(IBuildService buildService, IPokemonService pokemonService, IHeldItemService heldItemService)
        {
            _buildService = buildService;
            _pokemonService = pokemonService;
            _heldItemService = heldItemService;
        }

        // GET: Build
        public async Task<IActionResult> Index()
        {
            var builds = await _buildService.ListBuilds();
            return View(builds);
        }

        // GET: Build/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var build = await _buildService.FindBuild(id);
            if (build == null) return NotFound();

            return View(build);
        }

        // GET: Build/Create
        public async Task<IActionResult> Create()
        {
            var pokemons = await _pokemonService.ListPokemons();
            var heldItems = await _heldItemService.ListHeldItems();

            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");
            ViewBag.HeldItems = new MultiSelectList(heldItems, "HeldItemId", "HeldItemName");

            return View();
        }

        // POST: Build/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Build build, int[] heldItemIds)
        {
            if (ModelState.IsValid)
            {
                // Attach Held Items to the build
                foreach (var heldItemId in heldItemIds)
                {
                    var heldItem = await _heldItemService.FindHeldItem(heldItemId);
                    if (heldItem != null)
                    {
                        build.HeldItems.Add(heldItem);
                    }
                }

                await _buildService.CreateBuild(build);
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns in case of error
            var pokemons = await _pokemonService.ListPokemons();
            var heldItems = await _heldItemService.ListHeldItems();

            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName");
            ViewBag.HeldItems = new MultiSelectList(heldItems, "HeldItemId", "HeldItemName");

            return View(build);
        }

        // GET: Build/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var build = await _buildService.FindBuild(id);
            if (build == null) return NotFound();

            var pokemons = await _pokemonService.ListPokemons();
            var heldItems = await _heldItemService.ListHeldItems();

            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName", build.PokemonId);
            ViewBag.HeldItems = new MultiSelectList(heldItems, "HeldItemId", "HeldItemName", build.HeldItems.Select(h => h.HeldItemId));

            return View(build);
        }

        // POST: Build/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Build build, int[] heldItemIds)
        {
            if (id != build.BuildId) return NotFound();

            if (ModelState.IsValid)
            {
                // Update Held Items
                var updatedHeldItems = new List<HeldItem>();
                foreach (var heldItemId in heldItemIds)
                {
                    var heldItem = await _heldItemService.FindHeldItem(heldItemId);
                    if (heldItem != null)
                    {
                        updatedHeldItems.Add(heldItem);
                    }
                }
                build.HeldItems = updatedHeldItems;

                var success = await _buildService.UpdateBuild(id, build);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns in case of error
            var pokemons = await _pokemonService.ListPokemons();
            var heldItems = await _heldItemService.ListHeldItems();

            ViewBag.Pokemons = new SelectList(pokemons, "PokemonId", "PokemonName", build.PokemonId);
            ViewBag.HeldItems = new MultiSelectList(heldItems, "HeldItemId", "HeldItemName", build.HeldItems.Select(h => h.HeldItemId));

            return View(build);
        }

        // GET: Build/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var build = await _buildService.FindBuild(id);
            if (build == null) return NotFound();

            return View(build);
        }

        // POST: Build/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _buildService.DeleteBuild(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
