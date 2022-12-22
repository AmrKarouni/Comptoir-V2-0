using COMPTOIR.Models.AppModels;

namespace COMPTOIR.Models.View
{
    public class ProductCategoryViewModel
    {
        public ProductCategoryViewModel()
        {

        }

        public ProductCategoryViewModel(ProductCategory model)
        {
            Id = model.Id;
            Name = model.Name;
            Code = model.Code;
            Description = model.Description;
            IsConsumable = model.IsConsumable;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool IsConsumable { get; set; }
    }

    public class ProductSubCategoryViewModel
    {
        public ProductSubCategoryViewModel()
        {

        }

        public ProductSubCategoryViewModel(ProductSubCategory model)
        {
            Id = model.Id;
            Name = model.Name;
            Code = model.Code;
            IsGarbage = model.IsGarbage;
            CategoryId = model.CategoryId;
            CategoryName = model.Category?.Name;
            CategoryCode = model.Category?.Code;
            IsConsumable = model.Category?.IsConsumable;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool? IsGarbage { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryCode { get; set; }
        public bool? IsConsumable { get; set; }
    }


    public class ProductViewModel
    {
        public ProductViewModel(Product product,string hostpath)
        {
            Id = product.Id;
            Name = product.Name;
            Code = product.Code;
            Manifacturer = product.Manifacturer;
            Description = product.Description;
            ImageUrl = hostpath + product.ImageUrl; ;
            IsFinal = product.IsFinal;
            IsRaw = product.IsRaw;
            CreatedDate = product.CreatedDate;
            UnitName = product.UnitName;
            SubCategoryId = product.SubCategoryId;
            SubCategoryName = product.SubCategory?.Name;
            SubCategoryCode = product.SubCategory?.Code;
            IsGarbage = product.SubCategory?.IsGarbage;
            CategoryId = product.SubCategory?.CategoryId;
            CategoryName = product.SubCategory?.Category?.Name;
            CategoryCode = product.SubCategory?.Category?.Code;
            IsConsumable = product.SubCategory?.Category?.IsConsumable;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Manifacturer { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFinal { get; set; }
        public bool IsRaw { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UnitName { get; set; }

        public int SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public string? SubCategoryCode { get; set; }
        public bool? IsGarbage { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryCode { get; set; }
        public bool? IsConsumable { get; set; }
    }

}
