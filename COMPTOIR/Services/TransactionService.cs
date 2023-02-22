using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _db;

        public TransactionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResultWithMessage> PostTransactionCategoryAsync(TransactionCategory model)
        {
            var cat = _db.TransactionCategories?.FirstOrDefault(x => x.Name == model.Name);
            if (cat != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Transaction Category {model.Name} Already Exist." };
            }
            await _db.TransactionCategories.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }

        public ResultWithMessage GetAllTransactionCategories()
        {
            var categories = _db.TransactionCategories?.Where(x => x.IsDeleted == false).ToList();
            return new ResultWithMessage { Success = true, Result = categories };
        }
    }
}
