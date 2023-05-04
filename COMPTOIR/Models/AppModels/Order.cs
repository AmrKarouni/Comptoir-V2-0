using COMPTOIR.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace COMPTOIR.Models.AppModels
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmationDate { get; set; } = DateTime.UtcNow;
        public DateTime? DoneDate { get; set; } = DateTime.UtcNow;
        public DateTime? CancelDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? CurrentDay { get; set; } = DateTime.UtcNow.Date;

        [ForeignKey("Terminal")]
        public int? TerminalId { get; set; }
        public virtual Terminal? Terminal { get; set; }

        [ForeignKey("PaymentChannel")]
        public int? PaymentChannelId { get; set; }
        public virtual PaymentChannel? PaymentChannel { get; set; }

        [ForeignKey("PaymentMethod")]
        public int? PaymentMethodId { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public string? OrderAddress { get; set; }

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

        [ForeignKey("CanceledUser")]
        public string? CanceledBy { get; set; }
        public virtual ApplicationUser? CanceledUser { get; set; }

        [ForeignKey("LastUpdateUser")]
        public string? LastUpdateBy { get; set; }
        public virtual ApplicationUser? LastUpdateUser { get; set; }

        [ForeignKey("RefundedUser")]
        public string? RefundedBy { get; set; }
        public virtual ApplicationUser? RefundedUser { get; set; }

        public bool? IsConfirmed { get; set; } = true;
        public bool? IsCanceled { get; set; } = false;
        public bool? IsDone { get; set; } = true;
        public bool? IsDelivered { get; set; } = false;
        public bool? IsVip { get; set; } = false;
        public string? OrderNumber { get; set; }
        public string? Note { get; set; }
        public double? DiscountValue { get; set; }
        public string? PromotionName { get; set; }
        public double? PromotionValue { get; set; }
        public bool IsPaid { get; set; } = false;
        public double? TotalAmount { get; set; } = 0;
        public double? TotalPaidAmount { get; set; } = 0;
        public bool IsPrinted { get; set; } = false;
        public virtual ICollection<OrderTax>? Taxes { get; set; }
        public virtual ICollection<OrderProduct>? Products { get; set; }
    }
}
