using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
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
        public Transaction CreateProduction(Recipe model)
        {
            throw new NotImplementedException();
        }
    }
}
