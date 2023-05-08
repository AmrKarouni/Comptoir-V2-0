using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class UnitService : IUnitService
    {
        private readonly ApplicationDbContext _db;
        public UnitService(ApplicationDbContext db ) 
        {
            _db = db;
        }

        public ResultWithMessage GetAllUnits()
        {
            var units = _db.Units.OrderBy(x => x.Id).ToList();
            return new ResultWithMessage { Success = true, Result = units };
        }

        public async Task<ResultWithMessage> PostUnitAsync(Unit model)
        {
            var unit = _db.Units?.FirstOrDefault(x => x.Id == model.Id || x.Name == model.Name);
            if (unit != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Unit {model.Name} Already Exist." };
            }
            await _db.Units.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }


    }
}
