using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }

        [Required]
        public string PokemonName { get; set; }

        [Required]
        public string PokemonRole { get; set; } // Example: Attacker, Defender

        [Required]
        public string PokemonStyle { get; set; } // Example: Melee, Ranged

        public int PokemonHP { get; set; }
        public int PokemonAttack { get; set; }
        public int PokemonDefense { get; set; }
        public int PokemonSpAttack { get; set; }
        public int PokemonSpDefense { get; set; }
        public int PokemonCDR { get; set; } // Cooldown Reduction

        public string PokemonImage { get; set; } // URL or Path to the image file

        // Relationships
        public ICollection<Build> Builds { get; set; }
        public ICollection<HeldItem> HeldItems { get; set; }
    }

    public class PokemonDTO
    {
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public string PokemonRole { get; set; }
        public string PokemonStyle { get; set; }
        public int PokemonHP { get; set; }
        public int PokemonAttack { get; set; }
        public int PokemonDefense { get; set; }
        public int PokemonSpAttack { get; set; }
        public int PokemonSpDefense { get; set; }
        public int PokemonCDR { get; set; }
        public string PokemonImage { get; set; } // URL or Path to the image file

        public List<BuildDTO> Builds { get; set; } = new List<BuildDTO>();
    }
}
