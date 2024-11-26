using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeldItemAPIController : ControllerBase
    {
        private readonly IHeldItemService _heldItemService;

        public HeldItemAPIController(IHeldItemService heldItemService)
        {
            _heldItemService = heldItemService;
        }

        // GET: api/HeldItemAPI
        [HttpGet]
        public IActionResult GetAll()
        {
            var heldItems = _heldItemService.GetAllHeldItems();
            return Ok(heldItems);
        }

        // GET: api/HeldItemAPI/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var heldItem = _heldItemService.GetHeldItemById(id);
            if (heldItem == null) return NotFound();
            return Ok(heldItem);
        }

        // POST: api/HeldItemAPI
        [HttpPost]
        public IActionResult Create([FromBody] HeldItemDTO heldItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdHeldItem = _heldItemService.AddHeldItem(heldItem);
            return CreatedAtAction(nameof(GetById), new { id = createdHeldItem.HeldItemId }, createdHeldItem);
        }

        // PUT: api/HeldItemAPI/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] HeldItemDTO heldItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != heldItem.HeldItemId) return BadRequest();

            var success = _heldItemService.UpdateHeldItem(heldItem);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/HeldItemAPI/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _heldItemService.DeleteHeldItem(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
