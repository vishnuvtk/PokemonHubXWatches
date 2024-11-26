using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildAPIController : ControllerBase
    {
        private readonly IBuildService _buildService;

        public BuildAPIController(IBuildService buildService)
        {
            _buildService = buildService;
        }

        // GET: api/BuildAPI/List
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Build>>> ListBuilds()
        {
            var builds = await _buildService.ListBuilds();
            return Ok(builds);
        }

        // GET: api/BuildAPI/Find/1
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<Build>> FindBuild(int id)
        {
            var build = await _buildService.FindBuild(id);
            if (build == null)
                return NotFound();

            return Ok(build);
        }

        // POST: api/BuildAPI/Create
        [HttpPost("Create")]
        public async Task<ActionResult<Build>> CreateBuild([FromBody] Build build)
        {
            if (build == null) return BadRequest();

            var createdBuild = await _buildService.CreateBuild(build);
            return CreatedAtAction(nameof(FindBuild), new { id = createdBuild.BuildId }, createdBuild);
        }

        // PUT: api/BuildAPI/Update/1
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateBuild(int id, [FromBody] Build build)
        {
            if (id != build.BuildId) return BadRequest();

            var success = await _buildService.UpdateBuild(id, build);
            if (!success) return NotFound();

            return NoContent();
        }

        // DELETE: api/BuildAPI/Delete/1
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBuild(int id)
        {
            var success = await _buildService.DeleteBuild(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
