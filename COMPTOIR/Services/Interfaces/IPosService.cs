using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IPosService
    {
        ResultWithMessage GetAllPosRecipes();
        Task<ResultWithMessage> PostPosTicket(TicketBindingModel model);
    }
}
