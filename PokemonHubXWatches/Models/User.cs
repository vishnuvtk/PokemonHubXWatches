using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;


        // Navigation property: One user can have many reservations
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();


    }

    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
    }
}
