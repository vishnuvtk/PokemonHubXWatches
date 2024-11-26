using System.Collections.Generic;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IHeldItemService
    {
        IEnumerable<HeldItemDTO> GetAllHeldItems();
        HeldItemDTO GetHeldItemById(int id);
        HeldItemDTO AddHeldItem(HeldItemDTO heldItem);
        bool UpdateHeldItem(HeldItemDTO heldItem);
        bool DeleteHeldItem(int id);
    }
}
