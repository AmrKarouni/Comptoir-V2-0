namespace COMPTOIR.Models.Binding
{
    public class ProductBindingModel
    {


    }

    public class ProductCategoryBindingModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool IsConsumable { get; set; }
    }
}
