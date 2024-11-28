using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WatchesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// This endpoint returns all Watches in the system.
        /// </summary>
        /// <returns>[{Watch},{Watch},{Watch}]</returns>
        /// <example>
        /// GET api/WatchesAPI/List -> 
        /// [
        ///   {
        ///     "watchID": 1,
        ///     "name": "Rolex",
        ///     "price": 5000,
        ///     "description": "Luxury watch",
        ///     "reservation": null
        ///   },
        ///   {
        ///     "watchID": 2,
        ///     "name": "Omega",
        ///     "price": 3000,
        ///     "description": "Classic watch",
        ///     "reservation": null
        ///   }
        /// ]
        /// </example>
        // GET: api/WatchesAPI/List
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Watch>>> GetWatches()
        {
            return await _context.Watches.ToListAsync();
        }


        /// <summary>
        /// This endpoint returns a Watch specified by its {id}.
        /// </summary>
        /// <param name="id">The Watch ID</param>
        /// <returns>{Watch}</returns>
        /// <example>
        /// GET api/WatchesAPI/Find/1 -> 
        /// {
        ///   "watchID": 1,
        ///   "name": "Rolex",
        ///   "price": 5000,
        ///   "description": "Luxury watch",
        ///   "reservation": null
        /// }
        /// </example>
        // GET: api/WatchesAPI/Find/{id}
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<Watch>> GetWatch(int id)
        {
            var watch = await _context.Watches.FindAsync(id);

            if (watch == null)
            {
                return NotFound();
            }

            return watch;
        }


        /// <summary>
        /// This endpoint updates a Watch specified by its {id}.
        /// </summary>
        /// <param name="id">The Watch ID</param>
        /// <param name="watch">The updated Watch object</param>
        /// <returns>204 No Content or 404 Not Found</returns>
        /// <example>
        /// PUT api/WatchesAPI/Update/1
        /// </example>
        // PUT: api/WatchesAPI/Update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutWatch(int id, Watch watch)
        {
            if (id != watch.WatchID)
            {
                return BadRequest();
            }

            _context.Entry(watch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        /// <summary>
        /// This endpoint adds a new Watch.
        /// </summary>
        /// <param name="watch">The Watch object to add</param>
        /// <returns>{Watch}</returns>
        /// <example>
        /// POST api/WatchesAPI/Add
        /// {
        ///   "name": "Seiko",
        ///   "price": 1000,
        ///   "description": "Affordable quality"
        /// }
        /// </example>
        // POST: api/WatchesAPI/Add
        [HttpPost("Add")]
        public async Task<ActionResult<Watch>> PostWatch(Watch watch)
        {
            _context.Watches.Add(watch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWatch", new { id = watch.WatchID }, watch);
        }


        /// <summary>
        /// This endpoint deletes a Watch specified by its {id}.
        /// </summary>
        /// <param name="id">The Watch ID</param>
        /// <returns>204 No Content or 404 Not Found</returns>
        /// <example>
        /// DELETE api/WatchesAPI/Delete/1
        /// </example>
        // DELETE: api/WatchesAPI/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteWatch(int id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch == null)
            {
                return NotFound();
            }

            _context.Watches.Remove(watch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WatchExists(int id)
        {
            return _context.Watches.Any(e => e.WatchID == id);
        }
    }
}

