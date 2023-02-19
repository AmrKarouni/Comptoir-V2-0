namespace COMPTOIR.Models.Binding
{
    public class TicketDeliverBindingModel
    {
        public int TicketId { get; set; }
        public double PaidAmount { get; set; }
        public string? Note { get; set; }
        public bool? IsCash { get; set; }
    }
}
