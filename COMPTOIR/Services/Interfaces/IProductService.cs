using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IProductService
    {
        ResultWithMessage GetProducts(FilterModel model);

        ResultWithMessage GetAllProductCategories();
        ResultWithMessage GetProductCategoryById(int id);
        Task<ResultWithMessage> PostProductCategoryAsync(ProductCategory model);
        Task<ResultWithMessage> PutProductCategoryAsync(int id, ProductCategory model);

        ResultWithMessage GetAllProductSubCategories();
        ResultWithMessage GetProductSubCategoryById(int id);
        Task<ResultWithMessage> PostProductSubCategoryAsync(ProductSubCategory model);
        Task<ResultWithMessage> PutProductSubCategoryAsync(int id, ProductSubCategory model);


        //ResultWithMessage GetAllProducts();
        ResultWithMessage GetProductById(int id);
        Task<ResultWithMessage> PostProductAsync(Product model);
        Task<ResultWithMessage> PutProductAsync(int id, Product model);

        Task<ResultWithMessage> CheckCategoryName(string value);
        Task<ResultWithMessage> CheckCategoryCode(string value);
        Task<ResultWithMessage> CheckSubCategoryName(string value);
        Task<ResultWithMessage> CheckSubCategoryCode(string value);
        Task<ResultWithMessage> CheckProductName(string value);
        Task<ResultWithMessage> CheckProductCode(string value);
    }
}
