using System.Collections.Generic;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IBuildService
    {
        IEnumerable<BuildDTO> GetAllBuilds();
        BuildDTO GetBuildById(int id);
        BuildDTO CreateBuild(BuildDTO build);
        bool UpdateBuild(BuildDTO build);
        bool DeleteBuild(int id);
        BuildDTO CalculateUpdatedStats(int pokemonId, List<int> heldItemIds);
    }
}
