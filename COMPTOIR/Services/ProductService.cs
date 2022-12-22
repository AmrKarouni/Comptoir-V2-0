using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ResultWithMessage GetProducts(FilterModel model)
        {
            var products = _db.Products?.Include(x => x.SubCategory)
                                        .ThenInclude(y => y.Category)
                                        .Where(z => z.IsDeleted == false)
                                        .Select(p => new ProductViewModel(p));
            if (model.SearchQuery != null)
            {
                if (int.TryParse(model.SearchQuery,out int code))
                {
                    products = products.Where(x => x.Code == model.SearchQuery || 
                                                   x.SubCategoryCode == model.SearchQuery ||
                                                   x.CategoryCode == model.SearchQuery);
                }
                else
                {
                    products = products.Where(x => x.Name == model.SearchQuery || 
                                                   x.SubCategoryName == model.SearchQuery || 
                                                   x.CategoryName == model.SearchQuery);
                }
            }
            var dataSize = products.Count();
            products = products.Skip(model.PageSize * model.PageIndex).Take(model.PageSize);
            var sortProperty = typeof(ProductViewModel).GetProperty(model?.Sort ?? "Id");
            if (products == null)
            {
                return new ResultWithMessage();
            }
            if (model?.Order == "asc")
            {
                products?.OrderBy(x => sortProperty.GetValue(x));
            }
            products?.OrderByDescending(x => sortProperty.GetValue(x));

            return new ResultWithMessage
            {
                Success = true,
                Message = "",
                Result = new ObservableData(products, dataSize)
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
            var cat = _db.ProductCategories?.Find(id);
            if (cat == null)
            {
                return new ResultWithMessage { Success = false , Message = $@"Product Category ID#{id} No Found !!!" };
            }
            return new ResultWithMessage { Success = true, Result = cat }; 
        }
        public async Task<ResultWithMessage> PostProductCategoryAsync(ProductCategory model)
        {
            var cat = _db.ProductCategories?.FirstOrDefault(x => x.Name == model.Name || x.Code == model.Code);
            if (cat.Name == model.Name)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Category {model.Name} Already Exist !!!" };
            }
            if (cat.Code == model.Code)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Category {model.Code} Already Exist !!!" };
            }
            await _db.ProductCategories.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }

        public async Task<ResultWithMessage> PutProductCategoryAsync(int id,ProductCategory model)
        {
            if (id != model.Id)
            {
                return new ResultWithMessage { Success = false, Message = $@"Bad Request" };
            }
            var cat = _db.ProductCategories?.Find(id);
            _db.Entry(cat).State = EntityState.Detached;
            if (cat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product Category {model.Name} Not Found !!!" };
            }
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
            var subcat = _db.ProductSubCategories?.Find(id);
            if (subcat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product SubCategory ID#{id} No Found !!!" };
            }
            return new ResultWithMessage { Success = true, Result = subcat };
        }
        public async Task<ResultWithMessage> PostProductSubCategoryAsync(ProductSubCategory model)
        {
            var subcat = _db.ProductSubCategories?.FirstOrDefault(x => x.Name == model.Name || x.Code == model.Code);
            if (subcat.Name == model.Name)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product SubCategory {model.Name} Already Exist !!!" };
            }
            if (subcat.Code == model.Code)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product SubCategory {model.Code} Already Exist !!!" };
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
            var subcat = _db.ProductSubCategories?.Find(id);
            _db.Entry(subcat).State = EntityState.Detached;
            if (subcat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product SubCategory {model.Name} Not Found !!!" };
            }
            subcat = model;
            _db.Entry(subcat).State = EntityState.Modified;
            _db.SaveChanges();
            _db.Entry(subcat).Reference(x => x.Category).Load();
            var subcatviewmodel = new ProductSubCategoryViewModel(subcat);
            return new ResultWithMessage { Success = true, Result = subcatviewmodel };
        }

        public ResultWithMessage GetProductById(int id)
        {
            var prod = _db.Products?.Find(id);
            if (prod == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product  ID#{id} No Found !!!" };
            }
            return new ResultWithMessage { Success = true, Result = prod };
        }
        public async Task<ResultWithMessage> PostProductAsync(Product model)
        {
            var prod = _db.Products?.FirstOrDefault(x => x.Name == model.Name || x.Code == model.Code);
            if (prod.Name == model.Name)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product {model.Name} Already Exist !!!" };
            }
            if (prod.Code == model.Code)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product {model.Code} Already Exist !!!" };
            }
            await _db.Products.AddAsync(model);
            _db.SaveChanges();
            var res = _db.Products.Include(x => x.SubCategory).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == model.Id);
            var prodviewmodel = new ProductViewModel(res);
            return new ResultWithMessage { Success = true, Result = prodviewmodel };
        }

        public async Task<ResultWithMessage> PutProductAsync(int id, Product model)
        {
            if (id != model.Id)
            {
                return new ResultWithMessage { Success = false, Message = $@"Bad Request" };
            }
            var prod = _db.Products?.Find(id);
            _db.Entry(prod).State = EntityState.Detached;
            if (prod == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Product {model.Name} Not Found !!!" };
            }
            prod = model;
            _db.Entry(prod).State = EntityState.Modified;
            _db.SaveChanges();
            var res = _db.Products.Include(x => x.SubCategory).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == model.Id);
            var prodviewmodel = new ProductViewModel(res);
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

        public async Task<ResultWithMessage> CheckCategoryCode(string value)
        {
            var user = _db.ProductCategories?.FirstOrDefault(x => x.Code.ToLower() == value.ToLower());
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

        public async Task<ResultWithMessage> CheckSubCategoryCode(string value)
        {
            var user = _db.ProductSubCategories?.FirstOrDefault(x => x.Code.ToLower() == value.ToLower());
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
            var user = _db.Products?.FirstOrDefault(x => x.Code.ToLower() == value.ToLower());
            if (user == null)
            {
                return new ResultWithMessage { Success = false };
            }
            return new ResultWithMessage { Success = true };
        }
    }
}
