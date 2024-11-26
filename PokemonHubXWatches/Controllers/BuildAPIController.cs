using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.ViewModels;
using System.Collections.Generic;

namespace PokemonHubXWatches.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildAPIController : ControllerBase
    {
        private readonly IBuildService _buildService;
        private readonly IPokemonService _pokemonService;
        private readonly IHeldItemService _heldItemService;

        public BuildAPIController(IBuildService buildService, IPokemonService pokemonService, IHeldItemService heldItemService)
        {
            _buildService = buildService;
            _pokemonService = pokemonService;
            _heldItemService = heldItemService;
        }

        // GET: api/BuildAPI
        [HttpGet]
        public IActionResult GetAll()
        {
            var builds = _buildService.GetAllBuilds();
            return Ok(builds);
        }

        // GET: api/BuildAPI/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var build = _buildService.GetBuildById(id);
            if (build == null) return NotFound();
            return Ok(build);
        }

        // POST: api/BuildAPI
        [HttpPost]
        public IActionResult Create([FromBody] BuildCreationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var build = _buildService.CalculateUpdatedStats(model.Pokemon.PokemonId, model.SelectedHeldItemIds);
            var savedBuild = _buildService.CreateBuild(build);
            return CreatedAtAction(nameof(GetById), new { id = savedBuild.BuildId }, savedBuild);
        }

        // POST: api/BuildAPI/CalculateStats
        [HttpPost("CalculateStats")]
        public IActionResult CalculateStats([FromBody] BuildCreationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedStats = _buildService.CalculateUpdatedStats(model.Pokemon.PokemonId, model.SelectedHeldItemIds);
            if (updatedStats == null) return BadRequest("Invalid Pokémon or Held Items.");
            return Ok(updatedStats);
        }

        // DELETE: api/BuildAPI/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _buildService.DeleteBuild(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
