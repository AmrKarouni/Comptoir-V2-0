using COMPTOIR.Contexts;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Services
{
    public class PosService : IPosService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PosService(ApplicationDbContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResultWithMessage GetAllPosRecipes()
        {
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var defaultProduction = int.Parse(_configuration.GetValue<string>("DefaultProduction"));
            var recipes = _db.Recipes?.Include(p => p.Product)
                                      .Where(r => r.PlaceId == defaultProduction
                                               && r.IsDeleted == false
                                               && r.IsDeactivated == false
                                               && r.IsHidden == false
                                               && r.Product.IsFinal == true
                                               && r.Product.IsDeleted == false).Select(z => new PosRecipesViewModel
                                               {
                                                   ProductId = z.Product.Id,
                                                   ProductName = z.Product.Name,
                                                   ProductCode = z.Product.Code,
                                                   ProductImageUrl = !string.IsNullOrEmpty(z.Product.ImageUrl) ? hostpath + z.Product.ImageUrl : "",
                                                   IsFinal = z.Product.IsFinal,
                                                   CreatedDate = z.Product.CreatedDate,
                                                   UnitName = z.Product.UnitName,
                                                   RecipeId = z.Id,
                                                   RecipeName = z.Name,
                                                   PlaceId = z.PlaceId,
                                                   Price = z.Price,
                                                   SubCategoryId = z.Product.SubCategoryId
                                               });
            var cat = _db.ProductCategories?.Where(x => x.IsDeleted == false).Select(x => new PosCategoriesViewModel
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                IsConsumable = x.IsConsumable,
                SubCategories = x.SubCategories.Where(y => y.IsDeleted == false && y.CategoryId == x.Id).Select(y => new PosSubCategoriesViewModel
                {
                    SubCategoryId = y.Id,
                    SubCategoryName = y.Name,
                    IsGarbage = y.IsGarbage,
                    Recipes = recipes.Where(r => r.SubCategoryId == y.Id).ToList()
                }).ToList()
            }).ToList();
            return new ResultWithMessage { Success = true, Result = cat };
        }
    }
}
