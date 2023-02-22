using COMPTOIR.Contexts;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.Identity;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using COMPTOIR.Settings;
using Email.Service.Models;
using Email.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace COMPTOIR.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IOptions<JWT> jwt,
                           ApplicationDbContext db,
                           IConfiguration configuration,
                           IEmailService emailService,
                           IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _db = db;
            _configuration = configuration;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public ApplicationUser GetById(string id)
        {
            return _db.Users.Find(id);
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            }
            .Union(roleClaims)
            .Union(userClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<ResultWithMessage> RegistrationAsync(RegistrationModel model)
        {
            var username = await _userManager.FindByNameAsync(model.UserName);
            if (username != null)
            {
                return new ResultWithMessage { Success = false, Message = "Username Already Exist." };
            }
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail != null)
            {
                return new ResultWithMessage { Success = false, Message = "Email Already Exist." };
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                IsPasswordChanged = true,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, COMPTOIR.Constants.Authorization.user_role.ToString());
                return new ResultWithMessage { Success = true, Message = $@"User {model.UserName} has been registered." };
            }
            else
            {
                return new ResultWithMessage { Success = false, Message = result.Errors.FirstOrDefault()?.Description };
            }


        }

        public async Task<ResultWithMessage> GenerateEmailConfirmTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "User Not Found."
                };
            }
            if (user.EmailConfirmed == true)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "Already Email Confirmed."
                };
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return new ResultWithMessage { Success = true, Result = token };
        }

        public async Task<ResultWithMessage> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ResultWithMessage { Success = false, Message = "User Not Found." };

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return new ResultWithMessage { Success = false, Message = result.ToString() };
            }
            return new ResultWithMessage { Success = true, Message = $"Email {email} Has Been Confirmed." };
        }

        public async Task<AuthenticationModel> LoginAsync(LoginModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || !user.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.IsEmailConfirmed = false;
                authenticationModel.Message = $@"No accounts registered with {model.UserName}";
                return authenticationModel;
            }
            var confimred = await _userManager.IsEmailConfirmedAsync(user);
            if (!confimred)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.IsEmailConfirmed = false;
                authenticationModel.Message = $@"This Account Email Not Confirmed Yet.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                authenticationModel.IsEmailConfirmed = user.EmailConfirmed;
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.TokenDurationM = _jwt.DurationInMinutes;
                authenticationModel.TokenExpiry = jwtSecurityToken.ValidTo;
                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                    authenticationModel.RefreshToken = activeRefreshToken.Token;
                    //Static Value
                    authenticationModel.RefreshTokenDurationM = 10 * 24 * 60;
                    authenticationModel.RefreshTokenExpiry = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = GenerateRefreshToken();
                    authenticationModel.RefreshToken = refreshToken.Token;
                    //Static Value
                    authenticationModel.RefreshTokenDurationM = 10 * 24 * 60;
                    authenticationModel.RefreshTokenExpiry = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    _db.Update(user);
                    _db.SaveChanges();
                }

                return authenticationModel;

            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.IsEmailConfirmed = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        public async Task<ResultWithMessage> ChangePasswordAsync(ChangePasswordModel model)
        {

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"No accounts registered with {model.UserName}" };
            }
            if (await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
            {
                user.IsPasswordChanged = true;
                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                return new ResultWithMessage { Success = true, Message = $@"Password Changed for {model.UserName}" };
            }
            return new ResultWithMessage { Success = false, Message = $"Incorrect Credentials for user {user.Email}." };
        }

        public async Task<ResultWithMessage> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResultWithMessage { Success = false, Message = $@"No accounts registered with {model.Email}" };
            }
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.Password);
            user.IsPasswordChanged = false;
            if (result.Succeeded)
            {
                return new ResultWithMessage { Success = true, Message = $@"Password Reset for {user.UserName}" };
            }
            return new ResultWithMessage { Success = false, Message = result.Errors.FirstOrDefault()?.Description };
        }

        public async Task<AuthenticationModel> RefreshTokenAsync(string token)
        {
            var authenticationModel = new AuthenticationModel();
            var user = _db.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.IsEmailConfirmed = false;
                authenticationModel.Message = $"Token did not match any user.";
                return authenticationModel;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.IsEmailConfirmed = false;
                authenticationModel.Message = $"Token Not Active.";
                return authenticationModel;
            }

            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;

            //Generate new Refresh Token and save to Database
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _db.Update(user);
            _db.SaveChanges();

            //Generates new jwt
            authenticationModel.IsAuthenticated = true;
            authenticationModel.IsAuthenticated = user.EmailConfirmed;
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.TokenDurationM = _jwt.DurationInMinutes;
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenDurationM = 10 * 24 * 60;
            authenticationModel.RefreshTokenExpiry = newRefreshToken.Expires;

            return authenticationModel;
        }

        public async Task<ResultWithMessage> DeactivateAccountAsync(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new ResultWithMessage() { Success = false, Message = $@"No accounts registered with {user.UserName}" };
            }
            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }
            user.IsActive = false;
            RevokeTokenById(userId);
            _db.Update(user);
            _db.SaveChanges();
            return new ResultWithMessage() { Success = true, Message = $@"Account {user.UserName} Deactivated." };
        }

        public async Task<ResultWithMessage> ActivateAccountAsync(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new ResultWithMessage() { Success = false, Message = $@"No accounts registered with {user.UserName}" };
            }
            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }
            user.IsActive = true;
            _db.Update(user);
            _db.SaveChanges();
            return new ResultWithMessage() { Success = true, Message = $@"Account {user.UserName} Deactivated." };
        }

        public async Task<ResultWithMessage> RevokeToken(string token)
        {
            var user = _db.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            // return false if no user found with token
            if (user == null)
            {
                new ResultWithMessage() { Success = false, Message = $@"No accounts registered with {user.UserName}" };
            }

            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            // return false if token is not active
            if (!refreshToken.IsActive)
            {
                new ResultWithMessage() { Success = false, Message = $@"Refresh Token ins not acive." };
            }
            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _db.Update(user);
            _db.SaveChanges();
            return new ResultWithMessage() { Success = true, Message = $@"Revoke Token Succeeded." };
        }

        public async Task<ResultWithMessage> RevokeTokenById(string userId)
        {
            var user = _db.Users.Include(u => u.RefreshTokens).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                new ResultWithMessage() { Success = false, Message = $@"No accounts registered with {user.UserName}." };
            }
            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }
            foreach (var refreshToken in user.RefreshTokens)
            {
                if (refreshToken.IsActive)
                {
                    refreshToken.Revoked = DateTime.UtcNow;
                }
            }
            _db.Update(user);
            _db.SaveChanges();
            return new ResultWithMessage() { Success = true, Message = $@"Revoke Token Succeeded." };
        }

        public async Task<ResultWithMessage> AddNewRole(string roleName)
        {
            var existRole = await _roleManager.FindByNameAsync(roleName);
            if (existRole != null)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Role {roleName} Is Not Exist." };
            }
            var role = new IdentityRole(roleName);
            var identityRole = await _roleManager.CreateAsync(role);
            if (identityRole != null)
            {
                return new ResultWithMessage() { Success = true, Message = $@"Role {roleName} Has Been Created." };
            }
            return new ResultWithMessage() { Success = false, Message = $@"Role {roleName} Is Not Created." };
        }


        public async Task<ResultWithMessage> CheckUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new ResultWithMessage { Success = false };
            }
            return new ResultWithMessage { Success = true };
        }

        public async Task<ResultWithMessage> CheckEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ResultWithMessage { Success = false };
            }
            return new ResultWithMessage { Success = true };
        }


        public async Task<ResultWithMessage> ForgetPassword(ForgetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }

            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token == null)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "You Can't Reset Your Password Now, Please Try Again Later"
                };
            }

            var param = new Dictionary<string, string?>
                                {
                                    {"token", token },
                                    {"email", model.Email }
                                };
            var callback = QueryHelpers.AddQueryString(model.ClientUri, param);

            var body = @$"<table style='border-collapse: collapse; width: 100%;'>
                            <tbody>
                            <tr>
                            <td colspan='2'><img style='width: 30%;' src='' alt='' /></td>
                            </tr>
                            <tr>
                            <td colspan='2'>
                            <h4>Dear Mr. / Ms. {user.UserName},&nbsp;</h4>
                            </td>
                            </tr>
                            <tr>
                            <td colspan='2'>You have been sent this email because you request Forget Password with this email account.<br />Please click on <a href='{callback}'>this link </a>to reset your account password.</td>
                            </tr>
                            <tr>
                            <td colspan='2'>
                            <p>Thank You ,</p>
                            <h4>Dune Candles Abu Dhabi</h4>
                            </td>
                            </tr>
                            </tbody>
                            </table>";

            var bodybuilder = new BodyBuilder();
            bodybuilder.HtmlBody = body;
            var ms = bodybuilder.ToMessageBody();
            var message =
                        new Message(new string[]
                        { model.Email! }, "Reset Password From Dune Candles Abu Dhabi", bodybuilder.ToMessageBody());
            _emailService.SendEmail(message);
            return new ResultWithMessage
            {
                Success = true
            };

        }

        public async Task<ResultWithMessage> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }

            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "You Can't Reset Your Password Now, Please Try Again Later"
                };
            }
            return new ResultWithMessage
            {
                Success = true
            };
        }

        public async Task<ResultWithMessage> GetAllUsers(ClaimsPrincipal user)
        {
            var list = new List<UserViewModel>();
            var users = _db.Users.Where(x => x.UserName != user.Identity.Name).ToList();
            foreach (var u in users)
            {

                var v = new UserViewModel();
                v.Id = u.Id;
                v.UserName = u.UserName;
                v.Email = u.Email;
                v.IsActive = u.IsActive;
                v.Roles = (await _userManager.GetRolesAsync(u));
                var isSuperuser = await IsSuperuser(u.Id);
                if (!isSuperuser)
                {
                    list.Add(v);
                }
            }
            return new ResultWithMessage { Success = true, Result = list };

        }

        public ResultWithMessage GetAllRoles()
        {
            var roles = _db.Roles.Where(x => x.Name != "SuperUser" ).ToList();
            return new ResultWithMessage
            {
                Success = true,
                Result = roles
            };
        }


        public async Task<ResultWithMessage> GetUserInfo(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "User Not Found"

                };
            }

            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }
            var userview = new UserViewModel();
            userview.Id = user.Id;
            userview.UserName = user.UserName;
            userview.Email = user.Email;
            userview.IsActive = user.IsActive;
            userview.Roles = (await _userManager.GetRolesAsync(user));
            return new ResultWithMessage { Success = true, Result = userview };

        }

        public async Task<ResultWithMessage> PutUserRoles(string id, UserWithRoles model)
        {
            if (id != model.Id)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "Invalid Model"

                };
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "User Not Found"

                };
            }

            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }

            var oldroles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldroles);
            await _userManager.AddToRolesAsync(user, model.Roles);
            var userview = new UserViewModel();
            userview.Id = user.Id;
            userview.UserName = user.UserName;
            userview.Email = user.Email;
            userview.Roles = (await _userManager.GetRolesAsync(user));
            return new ResultWithMessage { Success = true, Result = userview };
        }

        public async Task<ResultWithMessage> UserResetPasswordAsync(UserResetPasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new ResultWithMessage
                {
                    Success = false,
                    Message = "User Not Found"

                };
            }

            var isSuperuser = await IsSuperuser(user.Id);
            if (isSuperuser)
            {
                return new ResultWithMessage() { Success = false, Message = $@"Unauthorized" };
            }
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
            user.IsPasswordChanged = false;
            if (result.Succeeded)
            {
                return new ResultWithMessage { Success = true, Message = $@"Password Reset for {user.UserName}" };
            }
            return new ResultWithMessage { Success = false, Message = result.Errors.FirstOrDefault()?.Description };
        }

        public async Task<ResultWithMessage> AddUserWithRolesAsync(RegistrationModel model)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail != null)
            {
                return new ResultWithMessage { Success = false, Message = "Email Already Exist." };
            }
            
            if (model.Roles.Contains("SuperUser"))
            {
                return new ResultWithMessage { Success = false, Message = "Unauthorized" };
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                IsPasswordChanged = true,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRolesAsync(user, model.Roles);
                return new ResultWithMessage { Success = true, Message = $@"User {model.UserName} has been registered." };
            }
            else
            {
                return new ResultWithMessage { Success = false, Message = result.Errors.FirstOrDefault()?.Description };
            }


        }

        public async Task<bool> IsAuthorized(ClaimsPrincipal user, string[] roles)
        {
            var dbuser = await _userManager.FindByNameAsync(user.Identity.Name);
            if (!dbuser.IsActive)
            {
                return false;
            }
            if (roles.FirstOrDefault(x => user.IsInRole(x)) == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsSuperuser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("SuperUser"))
            {
                return true;
            }

            return false;
        }
    }
}
