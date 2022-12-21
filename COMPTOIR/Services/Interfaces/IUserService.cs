using COMPTOIR.Models.Identity;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetById(string id);
        Task<ResultWithMessage> RegistrationAsync(RegistrationModel model);
        Task<AuthenticationModel> LoginAsync(LoginModel model);
        Task<ResultWithMessage> ChangePasswordAsync(ChangePasswordModel model);
        Task<ResultWithMessage> ResetPasswordAsync(ResetPasswordModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        ResultWithMessage DeactivateAccountAsync(string userId);
        ResultWithMessage RevokeToken(string token);
        ResultWithMessage RevokeTokenById(string userId);
        Task<ResultWithMessage> AddNewRole(string roleName);
    }
}
