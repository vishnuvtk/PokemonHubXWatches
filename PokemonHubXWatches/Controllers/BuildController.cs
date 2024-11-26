using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;

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

        // GET: /Build
        public IActionResult Index()
        {
            var builds = _buildService.GetAllBuilds();
            return View(builds); // Renders the Index view with Build list
        }

        // GET: /Build/Details/{id}
        public IActionResult Details(int id)
        {
            var build = _buildService.GetBuildById(id);
            if (build == null) return NotFound();

            var pokemon = _pokemonService.GetPokemonById(build.PokemonId);
            var heldItems = _heldItemService.GetAllHeldItems();

            var viewModel = new BuildDetailsViewModel
            {
                Build = build,
                Pokemon = pokemon,
                HeldItems = heldItems
            };

            return View(viewModel); // Renders the Details view for a specific Build
        }

        // GET: /Build/Create
        public IActionResult Create(int pokemonId)
        {
            var pokemon = _pokemonService.GetPokemonById(pokemonId);
            if (pokemon == null) return NotFound();

            var heldItems = _heldItemService.GetAllHeldItems();

            var viewModel = new BuildCreationViewModel
            {
                Pokemon = pokemon,
                AvailableHeldItems = heldItems
            };

            return View(viewModel); // Renders the Create form
        }

        // POST: /Build/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BuildCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var build = _buildService.CalculateUpdatedStats(model.Pokemon.PokemonId, model.SelectedHeldItemIds);
                var savedBuild = _buildService.CreateBuild(build);
                return RedirectToAction(nameof(Details), new { id = savedBuild.BuildId });
            }

            model.AvailableHeldItems = _heldItemService.GetAllHeldItems();
            return View(model); // Return to Create form with validation errors
        }

        // GET: /Build/Delete/{id}
        public IActionResult Delete(int id)
        {
            var build = _buildService.GetBuildById(id);
            if (build == null) return NotFound();
            return View(build); // Renders the Delete confirmation view
        }

        // POST: /Build/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var success = _buildService.DeleteBuild(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index)); // Redirect to Build list
        }
    }
}
