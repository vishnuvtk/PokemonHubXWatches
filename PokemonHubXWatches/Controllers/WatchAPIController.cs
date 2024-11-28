using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchesAPIController : ControllerBase
    {
        private readonly IWatchService _watchService;

        public WatchesAPIController(IWatchService watchService)
        {
            _watchService = watchService;
        }

        // GET: api/WatchesAPI
        [HttpGet]
        public async Task<ActionResult> GetWatches()
        {
            var watches = await _watchService.GetAllWatches();
            return Ok(watches);
        }

        // GET: api/WatchesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetWatch(int id)
        {
            var watch = await _watchService.GetWatchById(id);
            if (watch == null) return NotFound();
            return Ok(watch);
        }

        // POST: api/WatchesAPI
        [HttpPost]
        public async Task<ActionResult> PostWatch(WatchDTO watchDTO)
        {
            await _watchService.CreateWatch(watchDTO);
            return CreatedAtAction(nameof(GetWatch), new { id = watchDTO.WatchID }, watchDTO);
        }

        // PUT: api/WatchesAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutWatch(int id, WatchDTO watchDTO)
        {
            if (id != watchDTO.WatchID) return BadRequest();
            await _watchService.UpdateWatch(id, watchDTO);
            return NoContent();
        }

        // DELETE: api/WatchesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWatch(int id)
        {
            await _watchService.DeleteWatch(id);
            return NoContent();
        }
    }
}
