using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace COMPTOIR.Models.AppModels
{
    public class OrderTax
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Tax Name")]
        public string? Name { get; set; }
        [Required]
        public double Rate { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }

        [ForeignKey("Tax")]
        public int? TaxId { get; set; }
        public virtual Tax? Tax { get; set; }
    }
}
