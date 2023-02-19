using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IChannelService
    {
        Task<ResultWithMessage> PostChannelCategoryAsync(ChannelCategory model);
        ResultWithMessage GetAllChannelCategories();
        Task<ResultWithMessage> PostChannelAsync(Channel model);
        ResultWithMessage GetAllChannels();
        ResultWithMessage GetChannelsByCategoryId(int id);

    }
}
