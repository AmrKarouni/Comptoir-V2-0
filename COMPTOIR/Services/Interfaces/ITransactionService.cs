using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<ResultWithMessage> PostTransactionCategoryAsync(TransactionCategory model);
        ResultWithMessage GetAllTransactionCategories();
    }
}
