using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.FileModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IProductService
    {
        ResultWithMessage GetAllUnits();
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
        Task<ResultWithMessage> PostProductAsync(ProductBindingModel model);
        Task<ResultWithMessage> UploadProductImgAsync(FileModel model);
        Task<ResultWithMessage> DeleteProductImgAsync(int id);
        Task<ResultWithMessage> PutProductAsync(int id, ProductBindingModel model);

        Task<ResultWithMessage> CheckCategoryName(string value);
        Task<ResultWithMessage> CheckSubCategoryName(string value);
        Task<ResultWithMessage> CheckProductName(string value);
        Task<ResultWithMessage> CheckProductCode(string value);

        ResultWithMessage GenerateCode();
    }
}
