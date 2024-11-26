using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class HeldItem
    {
        [Key]
        public int HeldItemId { get; set; }

        [Required]
        public string HeldItemName { get; set; }

        public int HeldItemHP { get; set; }
        public int HeldItemAttack { get; set; }
        public int HeldItemDefense { get; set; }
        public int HeldItemSpAttack { get; set; }
        public int HeldItemSpDefense { get; set; }
        public int HeldItemCDR { get; set; }

        public string HeldItemImage { get; set; } // URL or Path to the image file

        // Relationships
        public ICollection<Pokemon> Pokemons { get; set; }
        public ICollection<Build> Builds { get; set; }
    }

    public class HeldItemDTO
    {
        public int HeldItemId { get; set; }
        public string HeldItemName { get; set; }
        public int HeldItemHP { get; set; }
        public int HeldItemAttack { get; set; }
        public int HeldItemDefense { get; set; }
        public int HeldItemSpAttack { get; set; }
        public int HeldItemSpDefense { get; set; }
        public int HeldItemCDR { get; set; }
        public string HeldItemImage { get; set; } // URL or Path to the image file

        public List<BuildDTO> Builds { get; set; } = new List<BuildDTO>();
    }
}
