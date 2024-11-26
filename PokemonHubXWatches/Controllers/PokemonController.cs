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

        // GET: Pokemon
        public async Task<IActionResult> Index()
        {
            var pokemons = await _pokemonService.ListPokemons();
            return View(pokemons);
        }

        // GET: Pokemon/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pokemon = await _pokemonService.FindPokemon(id);
            if (pokemon == null) return NotFound();

            return View(pokemon);
        }

        // GET: Pokemon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokemon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PokemonName,PokemonRole,PokemonStyle,PokemonHP,PokemonAttack,PokemonDefense,PokemonSpAttack,PokemonSpDefense,PokemonCDR,PokemonImage")] Pokemon pokemon)
        {
            if (ModelState.IsValid)
            {
                await _pokemonService.CreatePokemon(pokemon);
                return RedirectToAction(nameof(Index));
            }
            return View(pokemon);
        }

        // GET: Pokemon/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pokemon = await _pokemonService.FindPokemon(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PokemonId,PokemonName,PokemonRole,PokemonStyle,PokemonHP,PokemonAttack,PokemonDefense,PokemonSpAttack,PokemonSpDefense,PokemonCDR,PokemonImage")] Pokemon pokemon)
        {
            if (id != pokemon.PokemonId)
            {
                return BadRequest("ID mismatch");
            }

            if (ModelState.IsValid)
            {
                var success = await _pokemonService.UpdatePokemon(id, pokemon);
                if (!success)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(pokemon);
        }

        // GET: Pokemon/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pokemon = await _pokemonService.FindPokemon(id);
            if (pokemon == null) return NotFound();

            return View(pokemon);
        }

        // POST: Pokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _pokemonService.DeletePokemon(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
