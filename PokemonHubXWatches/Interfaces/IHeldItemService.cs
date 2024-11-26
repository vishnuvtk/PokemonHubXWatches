using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IHeldItemService
    {
        Task<IEnumerable<HeldItem>> ListHeldItems();
        Task<HeldItem> FindHeldItem(int id);
        Task<HeldItem> CreateHeldItem(HeldItem heldItem);
        Task<bool> UpdateHeldItem(int id, HeldItem heldItem);
        Task<bool> DeleteHeldItem(int id);
    }
}
