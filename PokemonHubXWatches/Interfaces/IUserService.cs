using PokemonHubXWatches.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int userId);
        Task CreateUser(UserDTO userDTO);
        Task UpdateUser(int userId, UserDTO userDTO);
        Task DeleteUser(int userId);
    }
}
