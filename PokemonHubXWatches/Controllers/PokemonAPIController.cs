using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonAPIController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonAPIController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        // GET: api/PokemonAPI/List
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Pokemon>>> ListPokemons()
        {
            var pokemons = await _pokemonService.ListPokemons();
            return Ok(pokemons);
        }

        // GET: api/PokemonAPI/Find/1
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<Pokemon>> FindPokemon(int id)
        {
            var pokemon = await _pokemonService.FindPokemon(id);
            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }

        // POST: api/PokemonAPI/Create
        [HttpPost("Create")]
        public async Task<ActionResult<Pokemon>> CreatePokemon([FromBody] Pokemon pokemon)
        {
            if (pokemon == null) return BadRequest();

            var createdPokemon = await _pokemonService.CreatePokemon(pokemon);
            return CreatedAtAction(nameof(FindPokemon), new { id = createdPokemon.PokemonId }, createdPokemon);
        }

        // PUT: api/PokemonAPI/Update/1
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon pokemon)
        {
            if (id != pokemon.PokemonId) return BadRequest();

            var success = await _pokemonService.UpdatePokemon(id, pokemon);
            if (!success) return NotFound();

            return NoContent();
        }

        // DELETE: api/PokemonAPI/Delete/1
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var success = await _pokemonService.DeletePokemon(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
