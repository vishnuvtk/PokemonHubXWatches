using PokemonHubXWatches.Models;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;

namespace PokemonHubXWatches.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new user
        public async Task<UserDTO> CreateUser(UserDTO userDTO)
        {
            var user = new User
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDTO.UserId = user.UserId;
            return userDTO;
        }

        // Get a user by ID
        public async Task<UserDTO> GetUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();

            return user;
        }

        // Get all users
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();

            return users;
        }

        // Delete a user by ID
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
