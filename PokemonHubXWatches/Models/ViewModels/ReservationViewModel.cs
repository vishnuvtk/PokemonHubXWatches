using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationID { get; set; }

        [Required]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Watch")]
        public int WatchID { get; set; }

        // Additional properties for display purpose
        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; } = string.Empty;

        [Display(Name = "Watch Name")]
        public string WatchName { get; set; } = string.Empty;
    }
}
