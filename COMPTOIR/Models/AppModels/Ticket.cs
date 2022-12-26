using COMPTOIR.Models.Binding;
using COMPTOIR.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class Ticket
    {
        public Ticket()
        {

        }
        public Ticket(TicketBindingModel model)
        {
            Id = model.Id;
            ChannelId = model.ChannelId;
            CustomerId = model.CustomerId;
            CustomerAddress = model.CustomerAddress;
            IsVip = model.IsVip;
            Note = model.Note;
            DiscountId = model.DiscountId;
            Transactions = new List<Transaction>();
        }

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmationDate { get; set; } = DateTime.UtcNow;
        public DateTime? DoneDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }
        public DateTime? OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? CurrentDay { get; set; } = DateTime.UtcNow.Date;
        [ForeignKey("Channel")]
        public int? ChannelId { get; set; }
        public virtual Channel? Channel { get; set; }
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public string? CustomerAddress { get; set; }

        [ForeignKey("IssuedUser")]
        public string? IssuedBy { get; set; }
        public virtual ApplicationUser? IssuedUser { get; set; }

        [ForeignKey("ConfirmedUser")]
        public string? ConfirmedBy { get; set; }
        public virtual ApplicationUser? ConfirmedUser { get; set; }

        [ForeignKey("DoneUser")]
        public string? DoneBy { get; set; }
        public virtual ApplicationUser? DoneUser { get; set; }

        [ForeignKey("ServedUser")]
        public string? ServedBy { get; set; }
        public virtual ApplicationUser? ServedUser { get; set; }

        [ForeignKey("DeliveredUser")]
        public string? DeliveredBy { get; set; }
        public virtual ApplicationUser? DeliveredUser { get; set; }

        //[ForeignKey("Captain")]
        public int? CaptainId { get; set; }
        //public Captain? Captain { get; set; }
  
        public bool? IsConfirmed { get; set; } = true;
        public bool? IsCancelled { get; set; } = false;
        public bool? IsDone { get; set; } = true;
        public bool? IsDelivered { get; set; } = false;
        public bool? IsVip { get; set; } = false;
        public int? TicketNumber { get; set; }
        public string? Note { get; set; }
        [ForeignKey("Discount")]
        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<Tax>? Taxes { get; set; }
        public virtual ICollection<TicketRecipe>? TicketRecipes { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }

    public class TicketRecipe
    {
        public TicketRecipe()
        {

        }

        public TicketRecipe(TicketRecipeBindingModel model)
        {
            RecipeId = model.RecipeId;
            PlaceId = model.PlaceId;
            Count = model.Count;
            Note = model.Note;
            IsFree = model.IsFree;
            UnitPrice = model.UnitPrice;
        }
        public int Id { get; set; }
        [ForeignKey("Ticket")]
        public int? TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }
        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }
        [ForeignKey("Place")]
        public int? PlaceId { get; set; }
        public virtual Place? Place { get; set; }
        public double Count { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsDone { get; set; }
        public bool? IsServed { get; set; }
        public string? Note { get; set; }
        public virtual ICollection<ExtraProduct>? ExtraProducts { get; set; }
        public bool? IsFree { get; set; } = false;
        public double? UnitPrice { get; set; } 
    }
}
