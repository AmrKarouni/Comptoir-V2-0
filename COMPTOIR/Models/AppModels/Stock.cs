using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace COMPTOIR.Models.AppModels
{
    public class Stock
    {
        public int Id { get; set; }
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public virtual Place? Place { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public double Amount { get; set; } = 0;
        public double UnitCost { get; set; } = 0;
    }
}
