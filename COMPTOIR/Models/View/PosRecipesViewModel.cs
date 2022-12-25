namespace COMPTOIR.Models.View
{

    public class PosCategoriesViewModel
    {
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsConsumable { get; set; }
        public IEnumerable<PosSubCategoriesViewModel>? SubCategories { get; set; }
    }

    public class PosSubCategoriesViewModel
    {
        public int SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public bool? IsGarbage { get; set; }
        public IEnumerable<PosRecipesViewModel>? Recipes { get; set; }
    }

    public class PosRecipesViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductCode { get; set; }
        public string? ProductImageUrl { get; set; }
        public bool IsFinal { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UnitName { get; set; }
        public int RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public int? PlaceId { get; set; }
        public double? Price { get; set; } = 0;
        public int? SubCategoryId { get; set; }

    }
}
