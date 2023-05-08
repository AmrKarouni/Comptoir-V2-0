using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _db;
        public DiscountService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResultWithMessage> PostDiscountAsync(Discount model)
        {
            var discount = _db.Discounts?.FirstOrDefault(x => x.Name == model.Name);
            if (discount != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Discount {model.Name} Already Exist." };
            }
            await _db.Discounts.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }
        public ResultWithMessage GetAllDiscounts()
        {
            var discounts = _db.Discounts.OrderBy(x => x.Name).ToList();
            return new ResultWithMessage { Success = true,Result = discounts };
        }
    }
}
