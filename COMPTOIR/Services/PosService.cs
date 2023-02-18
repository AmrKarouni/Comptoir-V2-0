﻿using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Services
{
    public class PosService : IPosService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PosService(ApplicationDbContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResultWithMessage GetAllPosRecipes()
        {
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var defaultProduction = int.Parse(_configuration.GetValue<string>("DefaultProduction"));
            var recipes = _db.Recipes?.Include(p => p.Product)
                                      .Where(r => r.PlaceId == defaultProduction
                                               && r.IsDeleted == false
                                               && r.IsDeactivated == false
                                               && r.IsHidden == false
                                               && r.Product.IsFinal == true
                                               && r.Product.IsDeleted == false).Select(z => new PosRecipesViewModel
                                               {
                                                   ProductId = z.Product.Id,
                                                   ProductName = z.Product.Name,
                                                   ProductCode = z.Product.Code,
                                                   ProductImageUrl = !string.IsNullOrEmpty(z.Product.ImageUrl) ? hostpath + z.Product.ImageUrl : "",
                                                   IsFinal = z.Product.IsFinal,
                                                   CreatedDate = z.Product.CreatedDate,
                                                   UnitName = z.Product.UnitName,
                                                   RecipeId = z.Id,
                                                   RecipeName = z.Name,
                                                   PlaceId = z.PlaceId,
                                                   Price = z.Price,
                                                   SubCategoryId = z.Product.SubCategoryId
                                               }).OrderBy(x => x.ProductCode);
            var cat = _db.ProductCategories?.Where(x => x.IsDeleted == false).Select(x => new PosCategoriesViewModel
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                IsConsumable = x.IsConsumable,
                SubCategories = x.SubCategories.Where(y => y.IsDeleted == false && y.CategoryId == x.Id).Select(y => new PosSubCategoriesViewModel
                {
                    SubCategoryId = y.Id,
                    SubCategoryName = y.Name,
                    IsGarbage = y.IsGarbage,
                    Recipes = recipes.Where(r => r.SubCategoryId == y.Id).ToList()
                }).ToList()
            }).ToList();
            return new ResultWithMessage { Success = true, Result = cat };
        }

        public async Task<ResultWithMessage> PostPosTicket(TicketBindingModel model)
        {
            if (model.ChannelId == null)
            {
                model.ChannelId = int.Parse(_configuration.GetValue<string>("DefaultChannel"));
            }
            if (model.CustomerId == null)
            {
                model.CustomerId = int.Parse(_configuration.GetValue<string>("DefaultCustomer"));
            }
            var ticket = new Ticket(model);

            ticket.TicketRecipes = model.Recipes.Select(x => new TicketRecipe(x)).ToList();
            ticket.Taxes = _db.Channels?.Include(x => x.Category)?
                                        .ThenInclude(x => x.Taxes)?
                                        .FirstOrDefault(x => x.Id == ticket.ChannelId).Category.Taxes?.ToList();
            ticket.TotalAmount = CalculateTicketAmount(ticket);
            ticket.TicketNumber = GenerateTicketNumber();

            await _db.Tickets.AddAsync(ticket);
            _db.SaveChanges();
            var q = _db.Tickets.Include(x => x.TicketRecipes)
                                .ThenInclude(x => x.Recipe)
                                .ThenInclude(x => x.Product)
                                .Include(x => x.Customer) 
                                .FirstOrDefault(x => x.Id == ticket.Id);
            var resTicket = new TicketBindingModel(q);
            return new ResultWithMessage { Success = true, Result = resTicket };
        }

        public async Task<ResultWithMessage> PutPosTicket(int id, TicketBindingModel model)
        {
            if (id != model.Id)
            {
                return new ResultWithMessage { Success = false, Message = "Bad Request !!" };
            }
            var ticket = _db.Tickets.Include(x => x.TicketRecipes)
                                    .Include(x => x.Taxes)
                                    .FirstOrDefault(x => x.Id == model.Id);
            if (ticket == null)
            {
                return new ResultWithMessage { Success = false, Message = "Ticket Not Found !!!" };
            }
            if (model.ChannelId == null)
            {
                model.ChannelId = int.Parse(_configuration.GetValue<string>("DefaultChannel"));
            }
            if (model.CustomerId == null)
            {
                model.CustomerId = int.Parse(_configuration.GetValue<string>("DefaultCustomer"));
            }
            
            ticket.TicketRecipes.Clear();
            ticket.Taxes.Clear();
            ticket.TicketRecipes = model.Recipes.Select(x => new TicketRecipe(x)).ToList();
            ticket.Taxes = _db.Channels?.Include(x => x.Category)?
                                    .ThenInclude(x => x.Taxes)?
                                    .FirstOrDefault(x => x.Id == model.ChannelId).Category.Taxes?.ToList();
            ticket.TotalAmount = CalculateTicketAmount(ticket);
            ticket.LastUpdateDate = DateTime.UtcNow;
            //ticket.LastUpdateBy = 
            _db.Entry(ticket).State = EntityState.Modified;
            _db.SaveChanges();
            var q = _db.Tickets.Include(x => x.TicketRecipes)
                                .ThenInclude(x => x.Recipe)
                                .ThenInclude(x => x.Product)
                                .Include(x => x.Customer)
                                .FirstOrDefault(x => x.Id == ticket.Id);
            var resTicket = new TicketBindingModel(q);
            return new ResultWithMessage { Success = true, Result = resTicket };
        }


        private double CalculateTicketAmount(Ticket ticket)
        {
            var amount = 0.0;
            ticket.TicketRecipes.Where(x => x.IsFree == false)
                                .Select(x => amount = amount + (x.UnitPrice * x.Count))
                                .ToList();
            if (ticket.Discount != null)
            {
                amount = (double)(amount - (amount * ticket.Discount));
            }
            if (ticket.Taxes != null)
            {
                amount = amount + (amount * ticket.Taxes.Sum(x => x.Rate));
            }
            return amount;
        }
        private string GenerateTicketNumber()
        {
            var date = DateTime.UtcNow.Date;
            var maxTicketNum = _db.Tickets.Where(x => x.CurrentDay == date).Count();
            var resut = date.Year.ToString().Substring(2, 2).PadLeft(2, '0')
                      + date.Month.ToString().PadLeft(2, '0')
                      + date.Day.ToString().PadLeft(2, '0')
                      + (maxTicketNum + 1).ToString().PadLeft(5, '0');
            return resut;
        }
        public async Task<ResultWithMessage> DeliverPosTicket(TicketDeliverBindingModel model)
        {
            var placeToId = int.Parse(_configuration.GetValue<string>("DefaultClientPlace"));
            var ticket = _db.Tickets.Include(x => x.TicketRecipes)
                                    .Include(x => x.Transactions)
                                    .ThenInclude(x => x.TransactionProducts)
                                    .FirstOrDefault(x => x.Id == model.TicketId
                                                    && x.IsConfirmed == true
                                                    && x.IsDone == true
                                                    && x.IsCancelled == false
                                                    && x.IsDelivered == false);
            if (ticket == null)
            {
                return new ResultWithMessage { Success = false, Message = "Ticket Not Found !!!" };
            }

            foreach (var ticketRecipe in ticket.TicketRecipes)
            {
                var recipe = _db.Recipes.Include(x => x.RecipeProducts).FirstOrDefault(x => x.Id == ticketRecipe.RecipeId);
                if (recipe == null)
                {
                    return new ResultWithMessage { Success = false, Message = "Recipe Not Found !!!" };
                }
                ticketRecipe.Recipe = recipe;
                ticket.Transactions.Add(new Transaction(recipe, ticketRecipe.Count));
            }
            ticket.Transactions = ticket.TicketRecipes.Select(x => new Transaction(x.Recipe, x.Count)).ToList();
            var channel = _db.Channels.Include(x => x.Category).FirstOrDefault(x => x.Id == ticket.ChannelId);
            if (channel == null)
            {
                return new ResultWithMessage { Success = false, Message = "Channel Not Found !!!" };
            }
            var fromPlaces = ticket.Transactions.Select(x => x.ToPlaceId).Distinct();
            foreach (var placeId in fromPlaces.ToList())
            {
                ticket.Transactions.Add(new Transaction(
                    placeId.Value, channel.Category.PlaceId.Value, "Transfer", ticket.Transactions.
                    Where(x => x.ToPlaceId == placeId).Select(p => new TransactionProduct
                    {
                        ProductId = p.ProductId,
                        Amount = p.ProductAmount.Value
                    }).ToList()));
            }

            var poschannel = _db.Channels.Include(x => x.Category).FirstOrDefault(x => x.Id == ticket.ChannelId);
            if (poschannel == null)
            {
                return new ResultWithMessage { Success = false, Message = "Channel Not Found !!!" };
            }
            var products = ticket.Transactions.Where(x => x.ToPlaceId == poschannel.Category.PlaceId.Value
                                                    && x.CategoryName == "Transfer")
                                            .SelectMany(x => x.TransactionProducts)
                                            .Select(x => new TransactionProduct { ProductId = x.ProductId, Amount = x.Amount })
                                            .ToList();
            ticket.Transactions.Add(new Transaction(
                poschannel.Category.PlaceId.Value, placeToId, "Sale", products));
            ticket.DeliveryDate = DateTime.UtcNow;
            ticket.IsDelivered = true;
            ticket.IsPaid = true;
            ticket.TotalPaidAmount = model.PaidAmount;
            ticket.Note = model.Note;
            _db.Entry(ticket).State = EntityState.Modified;
            _db.SaveChanges();
            var q = _db.Tickets.Include(x => x.TicketRecipes)
                                .ThenInclude(x => x.Recipe)
                                .ThenInclude(x => x.Product)
                                .Include(x => x.Customer)
                                .FirstOrDefault(x => x.Id == ticket.Id);
            var resTicket = new TicketBindingModel(q);
            return new ResultWithMessage { Success = true, Result = resTicket };
        }

        public ResultWithMessage GetTicketById(int id)
        {
            var ticket = _db.Tickets.Include(x => x.TicketRecipes)
                                .ThenInclude(x => x.Recipe)
                                .ThenInclude(x => x.Product)
                                .Include(x => x.Customer)
                                .FirstOrDefault(x => x.Id == id);
            if (ticket == null)
            {
                return new ResultWithMessage { Success = false, Message = "Ticket Not Found !!!" };
            }

            var resTicket = new TicketBindingModel(ticket);
            return new ResultWithMessage { Success = true, Result = resTicket };
        }

        public ResultWithMessage GetTodayPendingTickets()
        {
            var channel = int.Parse(_configuration.GetValue<string>("DefaultChannel"));
            var date = DateTime.UtcNow.Date;
            var tickets = _db.Tickets
                               .Include(x => x.Customer)
                               .Where(x => x.ChannelId == channel 
                                && x.CurrentDay == date
                                && x.IsPaid == false
                                && x.IsDelivered == false
                                && x.IsCancelled == false
                                && x.IsConfirmed == true
                                && x.IsDone == true)
                               .OrderBy(x => x.OrderDate);
            if (tickets == null)
            {
                return new ResultWithMessage { Success = false, Message = "No Ticket Found !!!" };
            }
            var q = tickets.Select(x => new TicketViewModel(x)).ToList();
            return new ResultWithMessage { Success = true, Result = q };

        }

        public ResultWithMessage GetTodayPendingTicketsByChannelId(int channelId)
        {
            var date = DateTime.UtcNow.Date;
            var tickets = _db.Tickets
                               .Include(x => x.Customer)
                               .Where(x => x.ChannelId == channelId
                                && x.CurrentDay == date
                                && x.IsPaid == false
                                && x.IsDelivered == false
                                && x.IsCancelled == false
                                && x.IsConfirmed == true
                                && x.IsDone == true)
                               .OrderBy(x => x.OrderDate);
            if (tickets == null)
            {
                return new ResultWithMessage { Success = false, Message = "No Ticket Found !!!" };
            }
            var q = tickets.Select(x => new TicketViewModel(x)).ToList();
            return new ResultWithMessage { Success = true, Result = q };

        }

        public ResultWithMessage GetPosTicketsByFilter(FilterModel model)
        {
            var list = new List<TicketViewModel>();
            var tickets = _db.Tickets.Include(x => x.Customer)
                                     .Include(x => x.TicketRecipes)
                                     .ThenInclude(y => y.Recipe)
                                     .ToList();
            if (model.IsVip != null)
            {
                tickets = tickets.Where(x => x.IsVip == model.IsVip).ToList();
            }

            if (model.IsPaid != null)
            {
                tickets = tickets.Where(x => x.IsPaid == model.IsPaid).ToList();
            }

            if (model.IsConfirmed != null)
            {
                tickets = tickets.Where(x => x.IsConfirmed == model.IsConfirmed).ToList();
            }

            if (model.IsCancelled != null)
            {
                tickets = tickets.Where(x => x.IsCancelled == model.IsCancelled).ToList();
            }

            if (model.IsDone != null)
            {
                tickets = tickets.Where(x => x.IsDone == model.IsDone).ToList();
            }


            if (model.IsDelivered != null)
            {
                tickets = tickets.Where(x => x.IsDelivered == model.IsDelivered).ToList();
            }

            if (model.HasDiscount != null)
            {
                tickets = tickets.Where(x => x.Discount > 0).ToList();
            }

            if (model.DateFrom.HasValue)
            {
                tickets = tickets?.Where(x => x.Date >= model.DateFrom).ToList();
            }
            if (model.DateTo.HasValue)
            {
                tickets = tickets?.Where(x => x.Date < model.DateTo.Value.AddDays(1)).ToList();
            }

            if (model.ConfirmationDateFrom.HasValue)
            {
                tickets = tickets?.Where(x => x.ConfirmationDate >= model.ConfirmationDateFrom).ToList();
            }
            if (model.ConfirmationDateTo.HasValue)
            {
                tickets = tickets?.Where(x => x.ConfirmationDate < model.ConfirmationDateTo.Value.AddDays(1)).ToList();
            }

            if (model.DoneDateFrom.HasValue)
            {
                tickets = tickets?.Where(x => x.DoneDate >= model.DoneDateFrom).ToList();
            }
            if (model.DoneDateTo.HasValue)
            {
                tickets = tickets?.Where(x => x.DoneDate < model.DoneDateTo.Value.AddDays(1)).ToList();
            }

            if (model.DeliveryDateFrom.HasValue)
            {
                tickets = tickets?.Where(x => x.DeliveryDate >= model.DeliveryDateFrom).ToList();
            }
            if (model.DeliveryDateTo.HasValue)
            {
                tickets = tickets?.Where(x => x.DeliveryDate < model.DeliveryDateTo.Value.AddDays(1)).ToList();
            }

            if (model.LastUpdateDateFrom.HasValue)
            {
                tickets = tickets?.Where(x => x.LastUpdateDate >= model.LastUpdateDateFrom).ToList();
            }
            if (model.LastUpdateDateTo.HasValue)
            {
                tickets = tickets?.Where(x => x.LastUpdateDate < model.LastUpdateDateTo.Value.AddDays(1)).ToList();
            }

            if (model.OrderDateFrom.HasValue)
            {
                tickets = tickets?.Where(x => x.OrderDate >= model.OrderDateFrom).ToList();
            }
            if (model.OrderDateTo.HasValue)
            {
                tickets = tickets?.Where(x => x.OrderDate < model.OrderDateTo.Value.AddDays(1)).ToList();
            }

            if (!string.IsNullOrEmpty(model.SearchQuery))
            {
                tickets = tickets?.Where(x => x.TicketRecipes.Any(y => y.Recipe.Name.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                        (x.Customer != null  && 
                                              (!string.IsNullOrEmpty(x.Customer.Name) && x.Customer.Name.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.Addresses01) &&  x.Customer.Addresses01.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.Addresses02) && x.Customer.Addresses02.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.Addresses03) && x.Customer.Addresses03.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.Addresses04) && x.Customer.Addresses04.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.Addresses05) && x.Customer.Addresses05.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.ContactNumber01) && x.Customer.ContactNumber01.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.ContactNumber02) && x.Customer.ContactNumber02.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.ContactNumber03) && x.Customer.ContactNumber03.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.ContactNumber04) && x.Customer.ContactNumber04.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Customer.ContactNumber05) && x.Customer.ContactNumber05.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.CustomerAddress) && x.CustomerAddress.ToLower().Contains(model.SearchQuery.ToLower()))))
                                  .ToList();
            }


            var dataSize = tickets.Count();
            var sortProperty = typeof(TicketViewModel).GetProperty(model?.Sort ?? "Id");
            if (model?.Order == "desc")
            {
                list = tickets?.Select(o => new TicketViewModel(o)).OrderByDescending(x => sortProperty.GetValue(x)).ToList();
            }
            else
            {
                list = tickets?.Select(o => new TicketViewModel(o)).OrderBy(x => sortProperty.GetValue(x)).ToList();
            }

            var result = list.Skip(model.PageSize * model.PageIndex).Take(model.PageSize).ToList();
            return new ResultWithMessage
            {
                Success = true,
                Result = new ObservableData(result, dataSize)
            };
        }
    }
}
