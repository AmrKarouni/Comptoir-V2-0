using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _db;
        public CustomerService(ApplicationDbContext db)
        {
            _db = db;
        }
        public ResultWithMessage GetCustomers(FilterModel model)
        {
            var customers = _db.Customers?.Where(z => z.IsDeleted == false);
            if (model.SearchQuery != null)
            {
                customers = customers?.Where(x => x.Name == model.SearchQuery ||
                                               x.Addresses01 == model.SearchQuery ||
                                               x.Addresses02 == model.SearchQuery ||
                                               x.Addresses03 == model.SearchQuery ||
                                               x.Addresses04 == model.SearchQuery ||
                                               x.Addresses05 == model.SearchQuery);
            }
            var dataSize = customers.Count();
            var sortProperty = typeof(ProductViewModel).GetProperty(model?.Sort ?? "Id");
            if (customers == null)
            {
                return new ResultWithMessage();
            }
            if (model?.Order == "asc")
            {
                customers?.OrderBy(x => sortProperty.GetValue(x));
            }
            else
            {
                customers?.OrderByDescending(x => sortProperty.GetValue(x));
            }
            
           var result = customers.Skip(model.PageSize * model.PageIndex).Take(model.PageSize);
            return new ResultWithMessage
            {
                Success = true,
                Message = "",
                Result = new ObservableData(result, dataSize)
            };
        }

        public ResultWithMessage GetCustomerById(int id)
        {
            var customer = _db.Customers?.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Customer ID#{id} No Found !!!" };
            }
            return new ResultWithMessage { Success = true, Result = customer };
        }

        public async Task<ResultWithMessage> PostCustomerAsync(Customer model)
        {
            var available = _db.Customers?.FirstOrDefault(x => x.Name == model.Name);
            if (available != null)
            {
                if (available.Name == model.Name)
                {
                    return new ResultWithMessage { Success = false, Message = $@"Customer {model.Name} Already Exist !!!" };
                }
            }
            await _db.Customers.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }
    }
}
