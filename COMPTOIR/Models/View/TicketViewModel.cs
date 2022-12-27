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
            CustomerName = model.Customer.Name;
            TotalAmount = model.TotalAmount;
        }
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CustomerName { get; set; }
        public double? TotalAmount { get; set; } = 0;
    }
}
