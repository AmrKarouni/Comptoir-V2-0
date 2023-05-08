using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationDbContext _db;
        public PlaceService(ApplicationDbContext db)
        {
            _db = db;
        }
        public ResultWithMessage GetPlaces()
        {
            var places = _db.Places.OrderBy(x => x.Name).ToList();
            return new ResultWithMessage { Success = true, Result = places };

        }
        public async Task<ResultWithMessage> PostPlaceAsync(Place model)
        {
            var place = _db.Places?.FirstOrDefault(x => x.Name == model.Name);
            if (place != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Place {model.Name} Already Exist." };
            }
            await _db.Places.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }
    }
}
