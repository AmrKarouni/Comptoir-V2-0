using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IPaymentChannelService
    {
        ResultWithMessage GetAllPaymentChannels();
        Task<ResultWithMessage> PostPaymentChannelsync(PaymentChannel model);
    }
}
