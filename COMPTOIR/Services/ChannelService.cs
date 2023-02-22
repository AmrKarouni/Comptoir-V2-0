using COMPTOIR.Contexts;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Services
{
    public class ChannelService : IChannelService
    {
        private readonly ApplicationDbContext _db;
        public ChannelService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResultWithMessage> PostChannelCategoryAsync(ChannelCategory model)
        {
            var cat = _db.ChannelCategories?.FirstOrDefault(x => x.Name == model.Name);
            if (cat != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Channel Category {model.Name} Already Exist." };
            }
            await _db.ChannelCategories.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }

        public ResultWithMessage GetAllChannelCategories()
        {
            var categories = _db.ChannelCategories?.Where(x => x.IsDeleted == false).ToList();
            return new ResultWithMessage { Success = true, Result = categories };
        }

        public async Task<ResultWithMessage> PostChannelAsync(Channel model)
        {
            var channel = _db.Channels?.FirstOrDefault(x => x.Name == model.Name);
            if (channel != null)
            {
                return new ResultWithMessage { Success = false, Message = $@"Channel {model.Name} Already Exist." };
            }
            await _db.Channels.AddAsync(model);
            _db.SaveChanges();
            return new ResultWithMessage { Success = true, Result = model };
        }

        public ResultWithMessage GetAllChannels()
        {
            var channels = _db.ChannelCategories?.Include(x => x.Channels.Where(x => x.IsDeleted == false))
                                                 .Where(x => x.IsDeleted == false)
                                                 .ToList();
            return new ResultWithMessage { Success = true, Result = channels };
        }
        public ResultWithMessage GetChannelsByCategoryId(int id)
        {
            var channels = _db.ChannelCategories?.Include(x => x.Channels.Where(x => x.IsDeleted == false))
                                                 .Where(x => x.IsDeleted == false && x.Id == id)
                                                 .ToList();
            return new ResultWithMessage { Success = true, Result = channels };
        }

    }
}
