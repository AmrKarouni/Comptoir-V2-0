using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IPaymentMethodService
    {
        ResultWithMessage GetAllPaymentMethods();
        Task<ResultWithMessage> PostPaymentMethodsync(PaymentMethod model);
    }
}
