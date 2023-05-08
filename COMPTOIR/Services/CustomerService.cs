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
            var list = new List<Customer>();
            var customers = _db.Customers.ToList();
            if (!string.IsNullOrEmpty(model.SearchQuery))
            {
                customers = customers?.Where(x =>
                                              (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Address01) && x.Address01.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Address02) && x.Address02.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.Address03) && x.Address03.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.ContactNumber01) && x.ContactNumber01.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.ContactNumber02) && x.ContactNumber02.ToLower().Contains(model.SearchQuery.ToLower())) ||
                                              (!string.IsNullOrEmpty(x.ContactNumber03) && x.ContactNumber03.ToLower().Contains(model.SearchQuery.ToLower())))
                                  .ToList();
            }


            var dataSize = customers.Count();
            var sortProperty = typeof(Customer).GetProperty(model?.Active ?? "Id");
            if (model?.Direction == "desc")
            {
                list = customers?.OrderByDescending(x => sortProperty.GetValue(x)).ToList();
            }
            else
            {
                list = customers?.OrderBy(x => sortProperty.GetValue(x)).ToList();
            }

            var result = list.Skip(model.PageSize * model.PageIndex).Take(model.PageSize).ToList();
            return new ResultWithMessage
            {
                Success = true,
                Result = new ObservableData(result, dataSize)
            };
        }

        public ResultWithMessage GetCustomerById(int id)
        {
            var customer = _db.Customers?.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Customer ID#{id} No Found." };
            }
            return new ResultWithMessage { Success = true, Result = customer };
        }

        public async Task<ResultWithMessage> PostCustomerAsync(Customer model)
        {
            var customer = _db.Customers?.FirstOrDefault(x => x.Name == model.Name);
            if (customer != null)
            {
                    return new ResultWithMessage { Success = false, Message = $@"Customer {model.Name} Already Exist." };
            }
            await _db.Customers.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }
    }
}
