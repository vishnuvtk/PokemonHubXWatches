using System.Collections.Generic;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.ViewModels
{
    public class BuildCreationViewModel
    {
        // The Pokémon for which the build is being created
        public PokemonDTO Pokemon { get; set; }

        // All available Held Items to choose from
        public IEnumerable<HeldItemDTO> AvailableHeldItems { get; set; }

        // The IDs of the selected Held Items for this build
        public List<int> SelectedHeldItemIds { get; set; }
    }
}
