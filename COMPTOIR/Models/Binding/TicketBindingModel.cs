namespace COMPTOIR.Models.Binding
{
    public class TicketBindingModel
    {
        public int Id { get; set; }
        public int? ChannelId { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerAddress { get; set; }
        public bool? IsVip { get; set; } = false;
        public string? Note { get; set; }
        public int? DiscountId { get; set; }
        public virtual IEnumerable<TicketRecipeBindingModel>? Recipes { get; set; }
    }

    public class TicketRecipeBindingModel
    {
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        
        public int? PlaceId { get; set; }
        public double Count { get; set; }
        public string? Note { get; set; }
        public bool? IsFree { get; set; } = false;
        public double? UnitPrice { get; set; }
    }
}
