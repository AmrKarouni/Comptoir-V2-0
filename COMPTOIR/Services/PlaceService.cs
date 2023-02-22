using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PlaceService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultWithMessage> PostPlaceCategoryAsync(PlaceCategory model)
        {
            var cat = _db.PlaceCategories?.FirstOrDefault(x => x.Name == model.Name);
            if (cat != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Place Category {model.Name} Already Exist." };
            }
            await _db.PlaceCategories.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }

        public ResultWithMessage GetAllPlaceCategories()
        {
            var categories = _db.PlaceCategories?.Where(x => x.IsDeleted == false).ToList();
            return new ResultWithMessage { Success = true, Result = categories };
        }

        public ResultWithMessage GetPlaceCategoryById(int id)
        {
            var cat = _db.PlaceCategories?.FirstOrDefault(x => x.Id == id);
            if (cat == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Place Category ID#{id} No Found." };
            }
            return new ResultWithMessage { Success = true, Result = cat };
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
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            _db.Entry(model).Reference(x => x.Category).Load();
            var subcatviewmodel = new PlaceViewModel(model, hostpath);
            return new ResultWithMessage { Success = true, Result = subcatviewmodel };
        }


        public ResultWithMessage GetAllPlaces()
        {
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var places = new List<PlaceViewModel>();
            places = _db.Places?.Include(x => x.Category)
                                .Where(x => x.IsDeleted == false)
                                              .Select(p => new PlaceViewModel(p, hostpath)).ToList();
            return new ResultWithMessage { Success = true, Result = places };
        }

        public ResultWithMessage GetPlaceById(int id)
        {
            var place = _db.Places.FirstOrDefault(x => x.Id == id);
            if (place == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Place ID#{id} No Found." };
            }
            return new ResultWithMessage { Success = true, Result = place };
        }

        public ResultWithMessage GetPlaceByCategoryId(int id)
        {
            var hostpath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var places = new List<PlaceViewModel>();
            places = _db.Places?.Include(x => x.Category)
                                .Where(x => x.IsDeleted == false && x.CategoryId == id)
                                              .Select(p => new PlaceViewModel(p, hostpath)).ToList();
            return new ResultWithMessage { Success = true, Result = places };
        }
    }
}
