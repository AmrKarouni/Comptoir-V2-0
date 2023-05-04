using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class OrderProduct
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public double Count { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsDone { get; set; }
        public bool? IsServed { get; set; }
        public string? Note { get; set; }
        public bool? IsFree { get; set; } = false;
        public double UnitPrice { get; set; } = 0;
    }
}
