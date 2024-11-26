using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> ListPokemons();
        Task<Pokemon> FindPokemon(int id);
        Task<Pokemon> CreatePokemon(Pokemon pokemon);
        Task<bool> UpdatePokemon(int id, Pokemon pokemon);
        Task<bool> DeletePokemon(int id);
    }
}
