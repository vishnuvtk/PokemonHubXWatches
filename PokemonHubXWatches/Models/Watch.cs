using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Watch
    {
        [Key]
        public int WatchID { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        // One-to-one relationship with Reservation
        public Reservation? Reservation { get; set; }

    }
    public class WatchDTO
    {
        public int WatchID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }

}

