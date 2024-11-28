namespace PokemonHubXWatches.Models.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public int UserID { get; set; }
        public string UserFullName { get; set; }
        public int WatchID { get; set; }
        public string WatchName { get; set; }
    }
}
