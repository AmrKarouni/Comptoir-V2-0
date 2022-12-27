using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IPosService
    {
        ResultWithMessage GetAllPosRecipes();
        Task<ResultWithMessage> PostPosTicket(TicketBindingModel model);
        Task<ResultWithMessage> PutPosTicket(int id,TicketBindingModel model);
        Task<ResultWithMessage> DeliverPosTicket(TicketDeliverBindingModel model);
        ResultWithMessage GetTicketById(int id);
        ResultWithMessage GetTodayPendingTickets();
        ResultWithMessage GetTodayPendingTicketsByChannelId(int channelId);
    }
}
