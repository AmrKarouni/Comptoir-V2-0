using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class Terminal
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        public string? Name { get; set; }
        public bool IsOccupied { get; set; } = false;
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public virtual Place? Place { get; set; }
    }
}
