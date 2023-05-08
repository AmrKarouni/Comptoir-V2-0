using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IPlaceService
    {
        ResultWithMessage GetPlaces();
        Task<ResultWithMessage> PostPlaceAsync(Place model);
    }
}
