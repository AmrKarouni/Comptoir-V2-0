using COMPTOIR.Models.AppModels;

namespace COMPTOIR.Models.View
{
    public class TicketViewModel
    {
        public TicketViewModel()
        {

        }
        public TicketViewModel(Ticket model)
        {
            Id = model.Id;
            TicketNumber = model.TicketNumber;
            CreatedDate = model.Date;
            LastUpdateDate = model.LastUpdateDate;
            RefundDate = model.RefundDate;
            CustomerName = model.Customer?.Name;
            ItemsCount = model.TicketRecipes?.Sum(x => x.Count);
            TotalAmount = model.TotalAmount;
            TotalPaidAmount = model.TotalPaidAmount;
            Discount = model.Discount;
            IsPaid = model.IsPaid;
            IsCash = model.IsCash;
            IsPrinted = model.IsPrinted;
            RefTicketId = model.RefTicketId;
            RefTicketNumber = model.RefTicketNumber;
            IsRefunded = model.IsRefunded;
            RefundTicketId = model.RefTicketId;
            RefundTicketNumber = model.RefundTicketNumber;
        }
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? RefundDate { get; set; }
        public string? CustomerName { get; set; }
        public double? TotalAmount { get; set; } = 0;
        public double? TotalPaidAmount { get; set; } = 0;
        public double? Discount { get; set;}
        public double? ItemsCount { get; set; }
        public bool IsPaid { get; set; }
        public bool? IsCash { get; set; }
        public bool? IsPrinted { get; set; }
        public int? RefTicketId { get; set; }
        public string? RefTicketNumber { get; set; }
        public bool IsRefunded { get; set; } = false;
        public int? RefundTicketId { get; set; }
        public string? RefundTicketNumber { get; set; }
    }
}
