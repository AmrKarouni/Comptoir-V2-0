using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace COMPTOIR.Models.AppModels
{
    public class Unit
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 1)]
        [Display(Name = "Unit Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
