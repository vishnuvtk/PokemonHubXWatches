using Microsoft.AspNetCore.Mvc;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersAPIController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/UsersAPI
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/UsersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/UsersAPI
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDTO userDTO)
        {
            await _userService.CreateUser(userDTO);
            return CreatedAtAction(nameof(GetUser), new { id = userDTO.UserId }, userDTO);
        }

        // PUT: api/UsersAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.UserId) return BadRequest();
            await _userService.UpdateUser(id, userDTO);
            return NoContent();
        }

        // DELETE: api/UsersAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
