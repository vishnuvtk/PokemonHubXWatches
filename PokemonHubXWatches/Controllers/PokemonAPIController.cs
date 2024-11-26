using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonAPIController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonAPIController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        // GET: api/PokemonAPI
        [HttpGet]
        public IActionResult GetAll()
        {
            var pokemons = _pokemonService.GetAllPokemon();
            return Ok(pokemons);
        }

        // GET: api/PokemonAPI/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pokemon = _pokemonService.GetPokemonById(id);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }

        // POST: api/PokemonAPI
        [HttpPost]
        public IActionResult Create([FromBody] PokemonDTO pokemon)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdPokemon = _pokemonService.AddPokemon(pokemon);
            return CreatedAtAction(nameof(GetById), new { id = createdPokemon.PokemonId }, createdPokemon);
        }

        // PUT: api/PokemonAPI/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PokemonDTO pokemon)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != pokemon.PokemonId) return BadRequest();

            var success = _pokemonService.UpdatePokemon(pokemon);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/PokemonAPI/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _pokemonService.DeletePokemon(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
