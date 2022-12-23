using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.FileModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IRecipeService _recipeService;
        public ProductService(ApplicationDbContext db,
                              IHttpContextAccessor httpContextAccessor,
                              IFileService fileService,
                              IRecipeService recipeService)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
            _recipeService = recipeService;
        }

        public ResultWithMessage GetAllUnits()
        {
            var list = _db.Units.Where(x => x.IsDeleted == false);
            return new ResultWithMessage { Success = true, Result = list };
        }

        public ResultWithMessage GetProducts(FilterModel model)
        {
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var products = _db.Products?.Include(x => x.SubCategory)
                                        .ThenInclude(y => y.Category)
                                        .Include(r => r.Recipes)
                                        .Where(z => z.IsDeleted == false);
            if (!string.IsNullOrEmpty(model.SearchQuery))
            {
                if (int.TryParse(model.SearchQuery, out int code))
                {
                    products = products.Where(x => x.Code == code);
                }
                else
                {
                    products = products.Where(x => x.Name.ToLower().StartsWith(model.SearchQuery.ToLower()) ||
                                                   x.SubCategory.Name.ToLower().StartsWith(model.SearchQuery.ToLower()) ||
                                                   x.SubCategory.Category.Name.ToLower().StartsWith(model.SearchQuery.ToLower()));
                }
            }
            var dataSize = products.Count();
            var sortProperty = typeof(ProductViewModel).GetProperty(model?.Sort ?? "Id");
            if (model?.Order == "asc")
            {
                products?.Select(p => new ProductViewModel(p, hostpath)).OrderBy(x => sortProperty.GetValue(x));
            }
            else
            {
                products?.Select(p => new ProductViewModel(p, hostpath))?.OrderByDescending(x => sortProperty.GetValue(x));
            }

            var result = products.Skip(model.PageSize * model.PageIndex).Take(model.PageSize).Select(p => new ProductViewModel(p, hostpath)).ToList();
            return new ResultWithMessage
            {
                Success = true,
                Message = "",
                Result = new ObservableData(result, dataSize)
            };
        }


        public ResultWithMessage GetAllProductCategories()
        {
            var categories = new List<ProductCategoryViewModel>();
            categories = _db.ProductCategories?.Where(x => x.IsDeleted == false)
                                              .Select(p => new ProductCategoryViewModel(p)).ToList();
            return new ResultWithMessage { Success = true, Result = categories };
        }

        public ResultWithMessage GetProductCategoryById(int id)
        {
            var cat = _db.ProductCategories?.FirstOrDefault(x => x.Id == id);
            if (cat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Category ID#{id} No Found !!!" };
            }
            return new ResultWithMessage { Success = true, Result = cat };
        }
        public async Task<ResultWithMessage> PostProductCategoryAsync(ProductCategory model)
        {
            var cat = _db.ProductCategories?.FirstOrDefault(x => x.Name == model.Name);
            if (cat != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Category {model.Name} Already Exist !!!" };
            }

            await _db.ProductCategories.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }

        public async Task<ResultWithMessage> PutProductCategoryAsync(int id, ProductCategory model)
        {
            if (id != model.Id)
            {
                return new ResultWithMessage { Success = false, Message = $@"Bad Request" };
            }
            var cat = _db.ProductCategories?.FirstOrDefault(x => x.Id == id);
            if (cat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Category {model.Name} Not Found !!!" };
            }
            _db.Entry(cat).State = EntityState.Detached;
            cat = model;
            _db.Entry(cat).State = EntityState.Modified;
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = cat };
        }

        public ResultWithMessage GetAllProductSubCategories()
        {
            var subcategories = new List<ProductSubCategoryViewModel>();
            subcategories = _db.ProductSubCategories?.Include(c => c.Category)
                                                .Where(x => x.IsDeleted == false)
                                                .Select(p => new ProductSubCategoryViewModel(p)).ToList();
            return new ResultWithMessage { Success = true, Result = subcategories };
        }

        public ResultWithMessage GetProductSubCategoryById(int id)
        {
            var subcat = _db.ProductSubCategories?.FirstOrDefault(x => x.Id == id);
            if (subcat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product SubCategory ID#{id} No Found !!!" };
            }
            return new ResultWithMessage { Success = true, Result = subcat };
        }
        public async Task<ResultWithMessage> PostProductSubCategoryAsync(ProductSubCategory model)
        {
            var subcat = _db.ProductSubCategories?.FirstOrDefault(x => x.Name == model.Name);
            if (subcat != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product SubCategory {model.Name} Already Exist !!!" };


            }
            await _db.ProductSubCategories.AddAsync(model);
            _db.SaveChanges();
            _db.Entry(model).Reference(x => x.Category).Load();
            var subcatviewmodel = new ProductSubCategoryViewModel(model);
            return new ResultWithMessage { Success = true, Result = subcatviewmodel };
        }

        public async Task<ResultWithMessage> PutProductSubCategoryAsync(int id, ProductSubCategory model)
        {
            if (id != model.Id)
            {
                return new ResultWithMessage { Success = false, Message = $@"Bad Request" };
            }
            var subcat = _db.ProductSubCategories?.FirstOrDefault(x => x.Id == id);
            if (subcat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product SubCategory {model.Name} Not Found !!!" };
            }
            _db.Entry(subcat).State = EntityState.Detached;
            subcat = model;
            _db.Entry(subcat).State = EntityState.Modified;
            _db.SaveChanges();
            _db.Entry(subcat).Reference(x => x.Category).Load();
            var subcatviewmodel = new ProductSubCategoryViewModel(subcat);
            return new ResultWithMessage { Success = true, Result = subcatviewmodel };
        }

        public ResultWithMessage GetProductById(int id)
        {
            var prod = _db.Products?.Include(x => x.Recipes)
                                    .Include(s => s.SubCategory)
                                    .ThenInclude(c => c.Category)
                                    .FirstOrDefault(x => x.Id == id);
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            if (prod == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product  ID#{id} No Found !!!" };
            }
            var res = new ProductViewModel(prod, hostpath);
            return new ResultWithMessage { Success = true, Result = res };
        }

        public async Task<ResultWithMessage> PostProductAsync(ProductBindingModel model)
        {
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var available = _db.Products?.FirstOrDefault(x => x.Name == model.Name || x.Code == model.Code);
            if (available != null)
            {
                if (available.Name == model.Name)
                {
                    return new ResultWithMessage { Success = false, Message = $@"Product {model.Name} Already Exist !!!" };
                }
                if (available.Code == model.Code)
                {
                    return new ResultWithMessage { Success = false, Message = $@"Product {model.Code} Already Exist !!!" };
                }
            }
            var prod = new Product(model);
            prod.Recipes.Add(_recipeService.InitialRecipe(prod, model.Price));
            await _db.Products.AddAsync(prod);
            _db.SaveChanges();
            var res = _db.Products.Include(x => x.SubCategory).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == prod.Id);
            var prodviewmodel = new ProductViewModel(res, hostpath);
            prodviewmodel.Price = model.Price;
            return new ResultWithMessage { Success = true, Result = prodviewmodel };
        }

        public async Task<ResultWithMessage> UploadProductImgAsync(FileModel model)
        {
            var productId = int.Parse(model.FileName);
            var product = _db.Products?.Find(productId);
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            if (product == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Not Found !!!" };
            }
            var uploadResult = await _fileService.UploadFile(model, "products");
            if (!uploadResult.Success)
            {
                return new ResultWithMessage { Success = false, Message = $@"Upload Logo Failed !!!" };
            }
            product.ImageUrl = uploadResult.Message;
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
            var result = new { LogoUrl = hostpath + uploadResult.Message };
            return new ResultWithMessage { Success = true, Result = result };
        }

        public async Task<ResultWithMessage> DeleteProductImgAsync(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Not Found !!!" };
            }
            var deletedfile = await _fileService.DeleteFile(product.ImageUrl);
            if (deletedfile == null || deletedfile.Success == false)
            {
                return new ResultWithMessage { Success = false, Message = $@"Delete Logo Failed !!!" };
            }
            product.ImageUrl = null;
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Message = "Logo Deleted !!!" };
        }

        public async Task<ResultWithMessage> PutProductAsync(int id, ProductBindingModel model)
        {
            if (id != model.Id)
            {
                return new ResultWithMessage { Success = false, Message = $@"Bad Request" };
            }
            var oldproduct = _db.Products?.FirstOrDefault(x => x.Id == id);
            if (oldproduct == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product {model.Name} Not Found !!!" };
            }
            _db.Entry(oldproduct).State = EntityState.Detached;
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var tempImgUrl = oldproduct.ImageUrl;
            var tempProd = new Product(model);
            var recipe = _db.Recipes.FirstOrDefault(x => x.ProductId == model.Id);
            if (recipe != null)
            {
                recipe.Price = model.Price;
                _db.Entry(recipe).State = EntityState.Modified;
            }
            tempProd.ImageUrl = tempImgUrl;
            _db.Entry(tempProd).State = EntityState.Modified;
            _db.SaveChanges();
            var res = _db.Products.Include(x => x.SubCategory).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == model.Id);
            var prodviewmodel = new ProductViewModel(res, hostpath);
            prodviewmodel.Price = model.Price;
            return new ResultWithMessage { Success = true, Result = prodviewmodel };
        }

        public async Task<ResultWithMessage> CheckCategoryName(string value)
        {
            var user = _db.ProductCategories?.FirstOrDefault(x => x.Name.ToLower() == value.ToLower());
            if (user == null)
            {
                return new ResultWithMessage { Success = false };
            }
            return new ResultWithMessage { Success = true };
        }

        public async Task<ResultWithMessage> CheckSubCategoryName(string value)
        {
            var user = _db.ProductSubCategories?.FirstOrDefault(x => x.Name.ToLower() == value.ToLower());
            if (user == null)
            {
                return new ResultWithMessage { Success = false };
            }
            return new ResultWithMessage { Success = true };
        }

        public async Task<ResultWithMessage> CheckProductName(string value)
        {
            var user = _db.Products?.FirstOrDefault(x => x.Name.ToLower() == value.ToLower());
            if (user == null)
            {
                return new ResultWithMessage { Success = false };
            }
            return new ResultWithMessage { Success = true };
        }

        public async Task<ResultWithMessage> CheckProductCode(string value)
        {
            var user = _db.Products?.FirstOrDefault(x => x.Code == int.Parse(value.ToLower()));
            if (user == null)
            {
                return new ResultWithMessage { Success = false };
            }
            return new ResultWithMessage { Success = true };
        }

        public ResultWithMessage GenerateCode()
        {
            var newcode = (_db.Products.Select(x => x.Code).Max() ??0) + 1 ;
            return new ResultWithMessage { Success = true, Result = newcode };
        }
    }
}
