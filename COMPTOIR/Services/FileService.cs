using COMPTOIR.Models.FileModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;

namespace COMPTOIR.Services
{
    public class FileService : IFileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public FileService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<ResultWithMessage> UploadFile(FileModel model, string path)
        {
            int MaxContentLength = 1024 * 1024 * 1; //Size = 5 MB
            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            var fileHostServer = _configuration.GetValue<string>("FileHostServer");
            if (model.File == null)
            {
                return new ResultWithMessage { Success = false, Message = "No File Found." };
            }
            var extension = model.File.FileName.Substring(model.File.FileName.LastIndexOf('.')).ToLower();
            if (!AllowedFileExtensions.Contains(extension))
            {
                return new ResultWithMessage { Success = false, Message = "Allowed Extensions are .jpg, .jpeg, .png " };
            }
            if (model.File.Length > MaxContentLength)
            {
                return new ResultWithMessage { Success = false, Message = "Max Size Allowed is 1 M.B" };
            }
            var filePath = Path.Combine(path + "/" + model.FileName + extension);
            var fullfilePath = Path.Combine(fileHostServer + "/", filePath);
            string directory = Path.GetDirectoryName(fullfilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            FileStream stream = new FileStream(fullfilePath, FileMode.Create);
            await model.File.CopyToAsync(stream);
            stream.Close();
            return new ResultWithMessage { Success = true, Message = "/" + filePath };
        }

        public async Task<ResultWithMessage> DeleteFile(string fileurl)
        {
            var fileHostServer = _configuration.GetValue<string>("FileHostServer");
            var fullfilePath = Path.Combine(fileHostServer + "/" + fileurl);
            if (File.Exists(fullfilePath))
            {
                File.Delete(fullfilePath);
                return new ResultWithMessage { Success = true, Message = $@"File {fileurl} Deleted." };
            }
            return new ResultWithMessage { Success = false, Message = $@"File {fileurl} Not Found." };
        }
    }
}
