using COMPTOIR.Models.AppModels;

namespace COMPTOIR.Services.Interfaces
{
    public interface ITransactionService
    {
        Transaction CreateProduction(Recipe model);
    }
}
