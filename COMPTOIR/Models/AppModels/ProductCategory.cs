using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace COMPTOIR.Models.AppModels
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ThumbUrl { get; set; }
        public string? Icon { get; set; }
        public int? SortOrder { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
