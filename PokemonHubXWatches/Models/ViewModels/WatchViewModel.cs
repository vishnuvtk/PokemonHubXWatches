using System.ComponentModel.DataAnnotations;

namespace PokemonHubXWatches.ViewModels
{
    public class WatchViewModel
    {
        public int WatchID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Watch Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;
    }
}
