using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class TaxService : ITaxService
    {
        private readonly ApplicationDbContext _db;
        public TaxService(ApplicationDbContext db) 
        {  
            _db = db; 
        }
        public ResultWithMessage GetAllTaxes()
        {
            var taxes = _db.Taxes.OrderBy(x => x.Name).ToList();
            return new ResultWithMessage { Success = true, Result =  taxes };
        }
        public async Task<ResultWithMessage> PostTaxAsync(Tax model)
        {
            var tax = _db.Taxes?.FirstOrDefault(x => x.Name == model.Name);
            if (tax != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Tax {model.Name} Already Exist." };
            }
            await _db.Taxes.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };

        }
    }
}
