using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface ICustomerService
    {
        ResultWithMessage GetCustomers(FilterModel model);
        ResultWithMessage GetCustomerById(int id);
        Task<ResultWithMessage> PostCustomerAsync(Customer model);
    }
}
