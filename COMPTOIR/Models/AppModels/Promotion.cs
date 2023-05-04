using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace COMPTOIR.Models.AppModels
{
    public class Promotion
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Promotion Name")]
        public string? Name { get; set; }
        public double Value { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public virtual Place? Place { get; set; }
    }
}
