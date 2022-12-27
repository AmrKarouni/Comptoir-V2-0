using COMPTOIR.Models.Binding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class Recipe
    {
        public Recipe()
        {
            RecipeProducts = new List<RecipeProduct>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Recipe Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public double Amount { get; set; } = 1;
        public string? CreatedBy { get; set; }
        [ForeignKey("Place")]
        public int? PlaceId { get; set; }
        public virtual Place? Place { get; set; }
        public double AcceptedMargin { get; set; } = 0;
        public bool Viceversa { get; set; } = false;
        public virtual ICollection<RecipeProduct>? RecipeProducts { get; set; }
        public double? Price { get; set; }
        public virtual ICollection<ExtraProduct>? ExtraProducts { get; set; }
        public bool IsDeactivated { get; set; } = false;
        public bool IsHidden { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class RecipeProduct
    {
        public RecipeProduct()
        {

        }
        public RecipeProduct(Recipe model)
        {
            Product = model.Product;
            Amount = 1;
        }
        public int Id { get; set; }
        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public double Amount { get; set; }
        public double UnitCost { get; set; } = 0;
    }
}
