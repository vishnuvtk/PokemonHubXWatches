using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }


        public DateTime ReservationDate { get; set; }


        // Foreign Key for User
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;


        // Foreign Key for Watch (One-to-One)
        public int WatchID { get; set; }
        public virtual Watch Watch { get; set; } = null!;
    }

    public class ReservationDTO
    {
        public int ReservationID { get; set; }

        public DateTime ReservationDate { get; set; }


        public int WatchID { get; set; }


        public int UserID { get; set; }

        public string UserFullName { get; set; } = string.Empty;
        public string WatchName { get; set; } = string.Empty;
    }
}
