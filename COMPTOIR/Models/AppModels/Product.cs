using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace COMPTOIR.Models.AppModels
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Product Code")]
        public long? Code { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ThumbUrl { get; set; }
        // Min stock value 
        public double? StockMinThreshold { get; set; }
        // Max stock value 
        public double? StockMaxThreshold { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("Unit")]
        public string? UnitName { get; set; }
        public virtual Unit? Unit { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public double UnitPrice { get; set; } = 0;
        public int? SortOrder { get; set; }
        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<Stock>? Stocks { get; set; }  

    }
}
