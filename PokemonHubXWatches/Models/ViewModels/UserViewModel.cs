namespace PokemonHubXWatches.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        // Display a list of reservation IDs only for simplicity
        public List<int> ReservationIds { get; set; } = new List<int>();
    }
}
