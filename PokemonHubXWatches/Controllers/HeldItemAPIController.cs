using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeldItemAPIController : ControllerBase
    {
        private readonly IHeldItemService _heldItemService;

        public HeldItemAPIController(IHeldItemService heldItemService)
        {
            _heldItemService = heldItemService;
        }

        // GET: api/HeldItemAPI/List
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<HeldItem>>> ListHeldItems()
        {
            var heldItems = await _heldItemService.ListHeldItems();
            return Ok(heldItems);
        }

        // GET: api/HeldItemAPI/Find/1
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<HeldItem>> FindHeldItem(int id)
        {
            var heldItem = await _heldItemService.FindHeldItem(id);
            if (heldItem == null)
                return NotFound();

            return Ok(heldItem);
        }

        // POST: api/HeldItemAPI/Create
        [HttpPost("Create")]
        public async Task<ActionResult<HeldItem>> CreateHeldItem([FromBody] HeldItem heldItem)
        {
            if (heldItem == null) return BadRequest();

            var createdHeldItem = await _heldItemService.CreateHeldItem(heldItem);
            return CreatedAtAction(nameof(FindHeldItem), new { id = createdHeldItem.HeldItemId }, createdHeldItem);
        }

        // PUT: api/HeldItemAPI/Update/1
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateHeldItem(int id, [FromBody] HeldItem heldItem)
        {
            if (id != heldItem.HeldItemId) return BadRequest();

            var success = await _heldItemService.UpdateHeldItem(id, heldItem);
            if (!success) return NotFound();

            return NoContent();
        }

        // DELETE: api/HeldItemAPI/Delete/1
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteHeldItem(int id)
        {
            var success = await _heldItemService.DeleteHeldItem(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
