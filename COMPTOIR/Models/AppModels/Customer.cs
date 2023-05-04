using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace COMPTOIR.Models.AppModels
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Customer Name")]
        public string? Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? ContactNumber01 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? ContactNumber02 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? ContactNumber03 { get; set; }
        public string? Gender { get; set; }
        public int? LoyalityLevel { get; set; }
        public string? Address01 { get; set; }
        public string? Address02 { get; set; }
        public string? Address03 { get; set; }
    }
}
