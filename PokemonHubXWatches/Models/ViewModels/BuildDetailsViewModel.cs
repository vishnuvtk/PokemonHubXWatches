using System.Collections.Generic;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.ViewModels
{
    public class BuildDetailsViewModel
    {
        // The build details
        public BuildDTO Build { get; set; }

        // The Pokémon associated with this build
        public PokemonDTO Pokemon { get; set; }

        // The Held Items attached to this build
        public IEnumerable<HeldItemDTO> HeldItems { get; set; }
    }
}
