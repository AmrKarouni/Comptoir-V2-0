namespace COMPTOIR.Models.View
{
    public class FilterModel
    {
        public string? SearchQuery { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Sort { get; set; }
        public string? Order { get; set; }
        ///
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public DateTime? ConfirmationDateFrom { get; set; }
        public DateTime? ConfirmationDateTo { get; set; }

        public DateTime? DoneDateFrom { get; set; }
        public DateTime? DoneDateTo { get; set; }

        public DateTime? DeliveryDateFrom { get; set; }
        public DateTime? DeliveryDateTo { get; set; }

        public DateTime? LastUpdateDateFrom { get; set; }
        public DateTime? LastUpdateDateTo { get; set; }

        public DateTime? OrderDateFrom { get; set; }
        public DateTime? OrderDateTo { get; set; }

        public DateTime? RefundDateFrom { get; set; }
        public DateTime? RefundDateTo { get; set; }
        ///
        public bool? IsPaid { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsCanceled { get; set; }
        public bool? IsDone { get; set; }
        public bool? IsDelivered { get; set; }
        public bool? IsVip { get; set; }
        public bool? IsCash { get; set; }
        public bool? IsRefunded { get; set; }
        //
        public bool? HasDiscount { get; set; }
    }
}
