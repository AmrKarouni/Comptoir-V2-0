namespace COMPTOIR.Models.Binding
{
    public class ProductBindingModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Manifacturer { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsFinal { get; set; } = true;
        public bool? IsRaw { get; set; } = false;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UnitName { get; set; }
        public int? SubCategoryId { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public double? Price { get; set; } = 0;

    }

}
