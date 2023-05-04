using System.ComponentModel.DataAnnotations;

namespace COMPTOIR.Models.AppModels
{
    public class PaymentChannel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Place>? Places { get; set; }
        public virtual ICollection<Tax>? Taxes { get; set; }
    }
}
