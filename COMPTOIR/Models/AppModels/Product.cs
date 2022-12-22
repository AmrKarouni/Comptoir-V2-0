using COMPTOIR.Models.Binding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }
        [StringLength(20, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 2)]
        [Display(Name = "Category Code")]
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool IsConsumable { get; set; } = true;
        public virtual ICollection<ProductSubCategory>? SubCategories { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ProductSubCategory
    {
        public ProductSubCategory()
        {

        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "SubCategory Name")]
        public string? Name { get; set; }
        [StringLength(20, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 2)]
        [Display(Name = "SubCategory Code")]
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool IsGarbage { get; set; } = false;
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Unit
    {
        [Key]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 1)]
        [Display(Name = "Unit Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Product
    {
        public Product()
        {

        }
        public Product(ProductBindingModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Code = model.Code;
            Manifacturer = model.Manifacturer;
            Description = model.Description;
            ImageUrl = model.ImageUrl;
            IsFinal = model.IsFinal;
            IsRaw = model.IsRaw;
            CreatedDate = model.CreatedDate;
            UnitName = model.UnitName;
            SubCategoryId = model.SubCategoryId;
            IsDeleted = model.IsDeleted;
            Recipes = new List<Recipe>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }
        [StringLength(10, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 2)]
        [Display(Name = "Product Code")]
        public string? Code { get; set; }
        public string? Manifacturer { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFinal { get; set; } = true;
        public bool IsRaw { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("Unit")]
        public string? UnitName { get; set; }
        public virtual Unit? Unit { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public virtual ProductSubCategory? SubCategory { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection <Recipe>? Recipes { get; set; }
}
}
