using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    public class PokemonController : Controller
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        // GET: /Pokemon
        public IActionResult Index()
        {
            var pokemons = _pokemonService.GetAllPokemon();
            return View(pokemons); // Renders the Index view with Pokémon list
        }

        // GET: /Pokemon/Details/{id}
        public IActionResult Details(int id)
        {
            var pokemon = _pokemonService.GetPokemonById(id);
            if (pokemon == null) return NotFound();
            return View(pokemon); // Renders the Details view for a specific Pokémon
        }

        // GET: /Pokemon/Create
        public IActionResult Create()
        {
            return View(); // Renders the Create form
        }

        // POST: /Pokemon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PokemonDTO pokemon)
        {
            if (ModelState.IsValid)
            {
                _pokemonService.AddPokemon(pokemon);
                return RedirectToAction(nameof(Index)); // Redirect to Pokémon list
            }
            return View(pokemon); // Return to Create form with validation errors
        }

        // GET: /Pokemon/Edit/{id}
        public IActionResult Edit(int id)
        {
            var pokemon = _pokemonService.GetPokemonById(id);
            if (pokemon == null) return NotFound();
            return View(pokemon); // Renders the Edit form
        }

        // POST: /Pokemon/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PokemonDTO pokemon)
        {
            if (ModelState.IsValid)
            {
                var success = _pokemonService.UpdatePokemon(pokemon);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index)); // Redirect to Pokémon list
            }
            return View(pokemon); // Return to Edit form with validation errors
        }

        // GET: /Pokemon/Delete/{id}
        public IActionResult Delete(int id)
        {
            var pokemon = _pokemonService.GetPokemonById(id);
            if (pokemon == null) return NotFound();
            return View(pokemon); // Renders the Delete confirmation view
        }

        // POST: /Pokemon/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var success = _pokemonService.DeletePokemon(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index)); // Redirect to Pokémon list
        }
    }
}
