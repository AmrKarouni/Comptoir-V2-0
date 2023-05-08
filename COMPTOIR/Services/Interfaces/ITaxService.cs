using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface ITaxService
    {
        ResultWithMessage GetAllTaxes();
        Task<ResultWithMessage> PostTaxAsync(Tax model);
    }
}
