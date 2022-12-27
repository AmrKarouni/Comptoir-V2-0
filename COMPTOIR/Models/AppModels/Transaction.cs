using COMPTOIR.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class TransactionCategory
    {
        [Key]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Transaction Name")]
        public string? Name { get; set; }
        public bool? IsViceversa { get; set; } = false;
        public bool? IsPayment { get; set; } = false;
        public string? InOut { get; set; }
        public bool? IsPos { get; set; } = true;
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
    public class Transaction
    {
        public Transaction()
        {

        }

        public Transaction(int placeFromId,int placeToId,string categoryName, List<TransactionProduct> transactionProducts)
        {
            CategoryName = categoryName;
            FromPlaceId = placeFromId;
            ToPlaceId = placeToId;
            Date = DateTime.UtcNow;
            TransactionProducts = transactionProducts;
        }

        public Transaction(Recipe recipe, double count)
        {
            CategoryName = "Production";
            FromPlaceId = recipe.PlaceId;
            ToPlaceId = recipe.PlaceId;
            Date = DateTime.UtcNow;
            RecipeId = recipe.Id;
            ProductId = recipe.ProductId;
            ProductAmount = recipe.Amount * count;
            RecipeCount = count;
            TransactionProducts = recipe.RecipeProducts.Select(x => new TransactionProduct(x, count)).ToList();
        }
        public int Id { get; set; }
        [ForeignKey("Category")]
        public string? CategoryName { get; set; }
        public virtual TransactionCategory? Category { get; set; }
        public DateTime? Date { get; set; } = DateTime.UtcNow;
        [ForeignKey("FromPlace")]
        public int? FromPlaceId { get; set; }
        public virtual Place? FromPlace { get; set; }
        [ForeignKey("ToPlace")]
        public int? ToPlaceId { get; set; }
        public virtual Place? ToPlace { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public double? ProductAmount { get; set; }
        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }
        public double? RecipeCount { get; set; }
        public string? Note { get; set; }
        public string? BillImageUrl { get; set; }
        public double? PayReceiveAmount { get; set; } = 0;
        [ForeignKey("Discount")]
        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public string? CustomerAddress { get; set; }

        [ForeignKey("IssuedUser")]
        public string? IssuedBy { get; set; }
        public virtual ApplicationUser? IssuedUser { get; set; }


        public bool? IsConfirmed { get; set; } = false;
        [ForeignKey("ConfirmedUser")]
        public string? ConfirmedBy { get; set; }
        public virtual ApplicationUser? ConfirmedUser { get; set; }
        public DateTime? ConfirmationDate { get; set; }


        public bool IsCancelled { get; set; } = false;
        [ForeignKey("CancelledUser")]
        public string? CancelledBy { get; set; }
        public virtual ApplicationUser? CancelledUser { get; set; }
        public DateTime? CancelDate { get; set; }

        public string? ValidOn { get; set; }
        public double? ProductUnitCost { get; set; }
        [ForeignKey("Ticket")]
        public int? TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }
     
        public virtual ICollection<TransactionProduct>? TransactionProducts { get; set; }
        public bool? IsArchive { get; set; } = false;
        public bool? IsBank { get; set; } = false;
    }

    public class TransactionProduct
    {
        public TransactionProduct()
        {

        }
        public TransactionProduct(RecipeProduct model, double count)
        {
            ProductId = model.ProductId;
            Amount = model.Amount * count;
            UnitPrice = model.UnitCost;
        }
        public int Id { get; set; }
        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public virtual Transaction? Transaction { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public double Amount { get; set; }
        public double UnitPrice { get; set; } = 0;
    }
}
