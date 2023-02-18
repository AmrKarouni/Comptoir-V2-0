using COMPTOIR.Models.AppModels;

namespace COMPTOIR.Models.Binding
{
    public class TicketBindingModel
    {
        public TicketBindingModel()
        {
        }
        public TicketBindingModel(Ticket ticket)
        {
            Id = ticket.Id;
            ChannelId = ticket.ChannelId;
            CustomerId = ticket.CustomerId;
            CustomerAddress = ticket.CustomerAddress;
            CustomerName = ticket.Customer?.Name;
            CreatedDate = ticket.Date;
            IsVip = ticket.IsVip;
            Note = ticket.Note;
            Discount = ticket.Discount;
            TicketNumber = ticket.TicketNumber; 
            Recipes = ticket.TicketRecipes.Select(x => new TicketRecipeBindingModel(x)).ToList();
        }
        public int Id { get; set; }
        public int? ChannelId { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsVip { get; set; } = false;
        public string? Note { get; set; }
        public double? Discount { get; set; }
        public string? TicketNumber { get; set; }
        public List<TicketRecipeBindingModel>? Recipes { get; set; }
    }

    public class TicketRecipeBindingModel
    {
        public TicketRecipeBindingModel()
        {

        }
        public TicketRecipeBindingModel(TicketRecipe model)
        {
            Id = model.Id;
            RecipeId = model.RecipeId;
            ProductName = model.Recipe.Product.Name;
            Count = model.Count;
            Note = model.Note;
            IsFree = model.IsFree;
            UnitPrice = model.UnitPrice;
        }
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        public string? ProductName { get; set; }
        public double Count { get; set; }
        public string? Note { get; set; }
        public bool? IsFree { get; set; } = false;
        public double UnitPrice { get; set; } = 0;
    }
}
