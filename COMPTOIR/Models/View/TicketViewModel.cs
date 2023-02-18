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
            CustomerName = model.Customer?.Name;
            ItemsCount = model.TicketRecipes?.Sum(x => x.Count);
            TotalAmount = model.TotalAmount;
            TotalPaidAmount = model.TotalPaidAmount;
            Discount = model.Discount;
            IsPaid = model.IsPaid;
        }
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string? CustomerName { get; set; }
        public double? TotalAmount { get; set; } = 0;
        public double? TotalPaidAmount { get; set; } = 0;
        public double? Discount { get; set;}
        public double? ItemsCount { get; set; }
        public bool IsPaid { get; set; }
    }
}
