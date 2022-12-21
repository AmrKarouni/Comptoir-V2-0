using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class ExtraProductCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }
        public virtual ICollection<ExtraProduct>? ExtraProducts { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ExtraProduct
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Extra Product Name")]
        public string? Name { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; } = 0;
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual ExtraProductCategory? Category { get; set; }
        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Extra Product Type")]
        public string? Type { get; set; }
        public bool IsDeleted { get; set; }
    }
}
