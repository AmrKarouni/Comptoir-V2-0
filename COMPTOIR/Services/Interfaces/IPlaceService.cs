using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<ResultWithMessage> PostPlaceCategoryAsync(PlaceCategory model);
        ResultWithMessage GetAllPlaceCategories();
        ResultWithMessage GetPlaceCategoryById(int id);

        Task<ResultWithMessage> PostPlaceAsync(Place model);
        ResultWithMessage GetAllPlaces();
        ResultWithMessage GetPlaceById(int id);
        ResultWithMessage GetPlaceByCategoryId(int id);

    }
}
