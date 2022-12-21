using System.ComponentModel.DataAnnotations;

namespace COMPTOIR.Models.AppModels
{
    public class Discount
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Discount Name")]
        public string? Name { get; set; }
        public double Value { get; set; }

    }
}
