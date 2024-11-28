namespace PokemonHubXWatches.Models.ViewModels
{
    public class WatchViewModel
    {
        public int WatchID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? ReservationId { get; set; } // Include reservation ID if there is a reservation
    }
}
