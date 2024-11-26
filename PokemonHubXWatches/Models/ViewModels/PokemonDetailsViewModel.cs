using System.Collections.Generic;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.ViewModels
{
    public class PokemonDetailsViewModel
    {
        // Pokémon details
        public PokemonDTO Pokemon { get; set; }

        // List of available Held Items for selection
        public IEnumerable<HeldItemDTO> AvailableHeldItems { get; set; }

        // List of IDs of selected Held Items (if any)
        public List<int> SelectedHeldItemIds { get; set; }

        // Calculated updated stats after attaching Held Items (optional for dynamic updates)
        public BuildDTO UpdatedStats { get; set; }
    }
}
