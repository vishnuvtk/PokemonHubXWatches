using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Build
    {
        [Key]
        public int BuildId { get; set; }

        public int PokemonUpdatedHP { get; set; }
        public int PokemonUpdatedAttack { get; set; }
        public int PokemonUpdatedDefense { get; set; }
        public int PokemonUpdatedSpAttack { get; set; }
        public int PokemonUpdatedSpDefense { get; set; }
        public int PokemonUpdatedCDR { get; set; }

        // Relationships
        public int PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; }

        public ICollection<HeldItem> HeldItems { get; set; }
    }

    public class BuildDTO
    {
        public int BuildId { get; set; }
        public int PokemonUpdatedHP { get; set; }
        public int PokemonUpdatedAttack { get; set; }
        public int PokemonUpdatedDefense { get; set; }
        public int PokemonUpdatedSpAttack { get; set; }
        public int PokemonUpdatedSpDefense { get; set; }
        public int PokemonUpdatedCDR { get; set; }

        public int PokemonId { get; set; }
        public PokemonDTO Pokemon { get; set; }

        public List<HeldItemDTO> HeldItems { get; set; } = new List<HeldItemDTO>();
    }
}
