using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IBuildService
    {
        Task<IEnumerable<Build>> ListBuilds();
        Task<Build> FindBuild(int id);
        Task<Build> CreateBuild(Build build);
        Task<bool> UpdateBuild(int id, Build build);
        Task<bool> DeleteBuild(int id);
    }
}
