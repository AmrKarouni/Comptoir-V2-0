using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly ApplicationDbContext _db;
        public PaymentMethodService (ApplicationDbContext db)
        {
            _db = db;
        }

        public ResultWithMessage GetAllPaymentMethods()
        {
            var pm = _db.PaymentMethods.OrderBy(x => x.Name).ToList();
            return new ResultWithMessage { Success = true, Result = pm };
        }

        public async Task<ResultWithMessage> PostPaymentMethodsync(PaymentMethod model)
        {
            var pm = _db.PaymentMethods?.FirstOrDefault(x => x.Name == model.Name);
            if (pm != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Payment Method {model.Name} Already Exist." };
            }
            await _db.PaymentMethods.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }
    }
}
