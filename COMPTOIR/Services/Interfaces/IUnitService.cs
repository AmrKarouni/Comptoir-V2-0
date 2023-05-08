using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IUnitService
    {
        ResultWithMessage GetAllUnits();
        Task<ResultWithMessage> PostUnitAsync(Unit model);
    }
}
