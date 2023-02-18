using COMPTOIR.Models.Binding;
using COMPTOIR.Models.Identity;
using COMPTOIR.Models.View;
using System.Security.Claims;

namespace COMPTOIR.Services.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetById(string id);
        Task<ResultWithMessage> RegistrationAsync(RegistrationModel model);
        Task<ResultWithMessage> GenerateEmailConfirmTokenAsync(string email);
        Task<ResultWithMessage> ConfirmEmail(string token, string email);
        Task<AuthenticationModel> LoginAsync(LoginModel model);
        Task<ResultWithMessage> ChangePasswordAsync(ChangePasswordModel model);
        Task<ResultWithMessage> ResetPasswordAsync(ResetPasswordModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        Task<ResultWithMessage> DeactivateAccountAsync(string userId);
        Task<ResultWithMessage> ActivateAccountAsync(string userId);
        Task<ResultWithMessage> RevokeToken(string token);
        Task<ResultWithMessage> RevokeTokenById(string userId);
        Task<ResultWithMessage> AddNewRole(string roleName);
        Task<ResultWithMessage> CheckUserName(string username);
        Task<ResultWithMessage> CheckEmail(string email);
        Task<ResultWithMessage> ForgetPassword(ForgetPasswordModel model);
        Task<ResultWithMessage> ResetPassword(ResetPasswordModel model);
        Task<ResultWithMessage> GetAllUsers(ClaimsPrincipal user);
        ResultWithMessage GetAllRoles();
        Task<ResultWithMessage> GetUserInfo(string id);
        Task<ResultWithMessage> PutUserRoles(string id, UserWithRoles model);
        Task<ResultWithMessage> UserResetPasswordAsync(UserResetPasswordModel model);
        Task<ResultWithMessage> AddUserWithRolesAsync(RegistrationModel model);
    }
}
