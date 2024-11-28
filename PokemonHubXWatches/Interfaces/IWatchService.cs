using PokemonHubXWatches.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonHubXWatches.Interfaces
{
    public interface IWatchService
    {
        Task<List<WatchDTO>> GetAllWatches();
        Task<WatchDTO> GetWatchById(int watchId);
        Task CreateWatch(WatchDTO watchDTO);
        Task UpdateWatch(int watchId, WatchDTO watchDTO);
        Task DeleteWatch(int watchId);
    }
}
