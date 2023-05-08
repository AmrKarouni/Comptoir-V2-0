using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Services
{
    public class PaymentChannelService : IPaymentChannelService
    {
        private readonly ApplicationDbContext _db;
        public PaymentChannelService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ResultWithMessage GetAllPaymentChannels()
        {
            var pc = _db.PaymentChannels.OrderBy(x => x.Name).ToList();
            return new ResultWithMessage { Success = true, Result = pc };
        }

        public async Task<ResultWithMessage> PostPaymentChannelsync(PaymentChannel model)
        {
            var pc = _db.PaymentChannels?.FirstOrDefault(x => x.Name == model.Name);
            if (pc != null)
            {
                    return new ResultWithMessage { Success = false, Message = $@"Payment Channel {model.Name} Already Exist." };
            }
            await _db.PaymentChannels.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }
    }
}
