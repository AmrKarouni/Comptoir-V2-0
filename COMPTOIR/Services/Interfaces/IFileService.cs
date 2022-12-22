using COMPTOIR.Models.FileModels;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IFileService
    {
        Task<ResultWithMessage> UploadFile(FileModel model, string path);
        Task<ResultWithMessage> DeleteFile(string fileurl);
    }
}
