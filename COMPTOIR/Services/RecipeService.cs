using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class RecipeService : IRecipeService
    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public RecipeService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public Recipe InitialRecipe(Product model,double price)
        {
            var recipe = new Recipe();
            recipe.Name = model.Name;
            recipe.Product = model;
            recipe.Amount = 1;
            recipe.PlaceId = int.Parse(_configuration.GetValue<string>("DefaultProduction"));
            recipe.Price = price;
            recipe.RecipeProducts?.Add(new RecipeProduct(recipe));
            return recipe;
        }
    }
}
