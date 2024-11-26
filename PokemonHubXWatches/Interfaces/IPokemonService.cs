using System.Collections.Generic;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IPokemonService
    {
        IEnumerable<PokemonDTO> GetAllPokemon();
        PokemonDTO GetPokemonById(int id);
        PokemonDTO AddPokemon(PokemonDTO pokemon);
        bool UpdatePokemon(PokemonDTO pokemon);
        bool DeletePokemon(int id);
    }
}
