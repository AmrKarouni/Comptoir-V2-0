using COMPTOIR.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime CurrentDay { get; set; }
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
  
        public bool IsConfirmed { get; set; } = false;
        public bool IsCancelled { get; set; } = false;
        public bool IsDone { get; set; } = false;
        public bool IsDelivered { get; set; } = false;
        public bool IsVip { get; set; } = false;
        public bool IsChild { get; set; } = false;
        public int? ParentId { get; set; }
        public int TicketNumber { get; set; }
        public string? Note { get; set; }
        [ForeignKey("Discount")]
        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<Tax>? Taxes { get; set; }
        public virtual ICollection<TicketRecipe>? TicketRecipes { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TicketRecipe
    {
        public int Id { get; set; }
        [ForeignKey("Ticket")]
        public int? TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }
        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public virtual Place? Place { get; set; }
        public double Count { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDone { get; set; }
        public bool IsServed { get; set; }
        public string? Note { get; set; }
        public virtual ICollection<ExtraProduct>? ExtraProducts { get; set; }
        public bool IsFree { get; set; } = false;
        public double UnitPrice { get; set; } = 0;
    }
}
