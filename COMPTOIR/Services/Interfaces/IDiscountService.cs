using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using System.Threading.Channels;

namespace COMPTOIR.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<ResultWithMessage> PostDiscountAsync(Discount model);
        ResultWithMessage GetAllDiscounts();
    }
}
