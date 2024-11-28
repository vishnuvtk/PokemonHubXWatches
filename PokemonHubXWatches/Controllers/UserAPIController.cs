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
    public class UsersAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This endpoint returns all Users in the system.
        /// </summary>
        /// <returns>[{User},{User},{User}]</returns>
        /// <example>
        /// GET api/UsersAPI/List -> 
        /// [
        ///   {
        ///     "userId": 13,
        ///     "userName": "pp",
        ///     "email": "pppp",
        ///     "fullName": "ljolj",
        ///     "address": "ppp",
        ///     "reservations": []
        ///   },
        ///   {
        ///     "userId": 14,
        ///     "userName": "ojojo",
        ///     "email": "ojojoj",
        ///     "fullName": "popkpj",
        ///     "address": "ojojoj",
        ///     "reservations": []
        ///   }
        /// ]
        /// </example>
        // GET: api/UsersAPI/List
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        /// <summary>
        /// This endpoint returns a User specified by its {id}.
        /// </summary>
        /// <param name="id">The User ID</param>
        /// <returns>{User}</returns>
        /// <example>
        /// GET api/UsersAPI/Find/1 -> 
        /// {
        ///   "userId": 1,
        ///   "userName": "john_doe",
        ///   "email": "john.doe@example.com",
        ///   "fullName": "John Doe",
        ///   "address": "123 Main St",
        ///   "reservations": [/* Reservation objects */]
        /// }
        /// </example>
        // GET: api/UsersAPI/Find/{id}
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        /// <summary>
        /// This endpoint updates a User specified by its {id}.
        /// </summary>
        /// <param name="id">The User ID</param>
        /// <param name="user">The updated User object</param>
        /// <returns>204 No Content or 404 Not Found</returns>
        /// <example>
        /// PUT api/UsersAPI/Update/1
        /// </example>
        // PUT: api/UsersAPI/Update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        /// This endpoint adds a new User.
        /// </summary>
        /// <param name="user">The User object to add</param>
        /// <returns>{User}</returns>
        /// <example>
        /// POST api/UsersAPI/Add
        /// {
        ///   "userName": "new_user",
        ///   "email": "new.user@example.com",
        ///   "fullName": "New User",
        ///   "address": "456 New Ave"
        /// }
        /// </example>
        // POST: api/UsersAPI/Add
        [HttpPost("Add")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }


        /// <summary>
        /// This endpoint deletes a User specified by its {id}.
        /// </summary>
        /// <param name="id">The User ID</param>
        /// <returns>204 No Content or 404 Not Found</returns>
        /// <example>
        /// DELETE api/UsersAPI/Delete/1
        /// </example>
        // DELETE: api/UsersAPI/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
