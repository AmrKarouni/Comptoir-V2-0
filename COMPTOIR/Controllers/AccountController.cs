using COMPTOIR.Models.Binding;
using COMPTOIR.Models.Identity;
using COMPTOIR.Services.Interfaces;
using Email.Service.Models;
using Email.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace COMPTOIR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(IUserService userService,
                                 IEmailService emailService,
                                 UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _emailService = emailService;
            _userManager = userManager; 
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegistrationAsync(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
            }
            var result = await _userService.RegistrationAsync(model);
            if (result.Success == false)
            {
                return BadRequest(new { message = result.Message });
            }
            if (result == null)
            {
                return BadRequest(new { message = "Email Already Exist" });
            }
            if (result.Success)
            {
                var res = await _userService.GenerateEmailConfirmTokenAsync(model.Email);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token = res.Result, email = model.Email }, Request.Scheme);


                var body = @$"<table style='border-collapse: collapse; width: 100%;'>
                            <tbody>
                            <tr>
                            <td colspan='2'><img style='width: 30%;' src='' alt='' /></td>
                            </tr>
                            <tr>
                            <td colspan='2'>
                            <h4>Dear Mr. / Ms. {model.UserName},&nbsp;</h4>
                            </td>
                            </tr>
                            <tr>
                            <td colspan='2'>You have been sent this email because you created an account on our website.<br />Please click on <a href='{confirmationLink}'>this link </a>to confirm your email address .</td>
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
                            { model.Email! }, "Confirmation Email From Dune Candles Abu Dhabi", bodybuilder.ToMessageBody());
                _emailService.SendEmail(message);
            }

            return Ok(result);
        }

        
        [HttpGet("resend-confirm-email")]
        public async Task<ActionResult> ResendConfirmEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            var generator = await _userService.GenerateEmailConfirmTokenAsync(email);
            if (!generator.Success)
            {
                return BadRequest(generator.Message);
            }
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token = generator.Result, email = email }, Request.Scheme);
            var body = (
                    "Dear Mr. / Ms. ," + user.UserName +" \n" +
                    "You have been sent this email because you created an account on our website.\n" +
                    "Please click on <a href =\"" + confirmationLink + "\"> this link </a> to confirm your email address is correct. ");
            var message =
                        new Message(new string[]
                        { email! }, "Confirmation Email From Dune Candles Abu Dhabi", body);
            _emailService.SendEmail(message);

            return Ok();
        }

        
        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail(string token, string email)
        {
            var result = await _userService.ConfirmEmail(token, email);
            if (result.Success == false)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Message);
        }

        
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginModel model)
        {
            var result = await _userService.LoginAsync(model);
            if (result.IsAuthenticated)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10)
            };
            Response.Cookies.Append("refreshToken", refreshToken.ToString(), cookieOptions);
        }

        //[Authorize(Roles = "Administrator,OfficeManager,DictionaryManager,SiteManager,ReportManager,BillingManager")]
        [HttpPost("changepassword")]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var result = await _userService.ChangePasswordAsync(model);
            if (result == null)
            {
                return BadRequest(new { message = "Change Password Failed!!!" });
            }
            return Ok(result);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            var result = await _userService.ResetPasswordAsync(model);
            if (result == null)
            {
                return BadRequest(new { message = "Reset Password Failed!!!" });
            }
            return Ok(result);
        }

        
        [HttpPost("refresh-token-cookies")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenParam(RefreshTokenBindingModel refreshToken)
        {
            var response = await _userService.RefreshTokenAsync(refreshToken.Token.ToString());
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpGet("deactivate-account/{id}")]
        public async Task<IActionResult> DeactivateAccountAsync(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return BadRequest(new { message = "User Id is required" });
            }
            var response = await _userService.DeactivateAccountAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Success);

            }
            return Ok(response.Success);

        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpGet("activate-account/{id}")]
        public async Task<IActionResult> ActivateAccountAsync(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return BadRequest(new { message = "User Id is required" });
            }
            var response = await _userService.ActivateAccountAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Success);

            }
            return Ok(response.Success);
        }


        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpGet("revoke-token/{id}")]
        public async Task<IActionResult> RevokeTokenById(string id)
        {
            // accept token from request body or cookie
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Id is required" });
            var response = await _userService.RevokeTokenById(id);
            if (!response.Success)
                return NotFound(new { message = "User not found" });
            return Ok(response);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpGet("addnewrole/{roleName}")]
        public async Task<IActionResult> AddNewRole(string roleName)
        {
            return Ok(await _userService.AddNewRole(roleName));
        }
        
        [HttpGet("check-username")]
        public async Task<IActionResult> CheckUserName(string username)
        {
            var response = await _userService.CheckUserName(username);
            return Ok(response.Success);
        }
        
        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail(string value)
        {
            var response = await _userService.CheckEmail(value);
            return Ok(response.Success);
        }

        
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Model");
            var result = await _userService.ForgetPassword(model);

            if (result.Success == false)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Success);
        }


        //[Authorize(Roles = "Administrator,OfficeManager,DictionaryManager,SiteManager,ReportManager,BillingManager,Office")]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Model");
            var result = await _userService.ResetPassword(model);

            if (result.Success == false)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Success);
        }


        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers(User);
            return Ok(result.Result);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpGet("roles")]
        public IActionResult GetAllRoles()
        {
            var result = _userService.GetAllRoles();
            return Ok(result.Result);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            var result = await _userService.GetUserInfo(id);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Result);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpPut("users/{id}")]
        public async Task<IActionResult> PutUserRoles(string id, UserWithRoles model)
        {
            var result = await _userService.PutUserRoles(id, model);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Result);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpPost("user-resetpassword")]
        public async Task<ActionResult> UserResetPasswordAsync(UserResetPasswordModel model)
        {
            var result = await _userService.UserResetPasswordAsync(model);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Success);
        }

        //[Authorize(Roles = "SuperUser,Administrator")]
        [HttpPost("add-user")]
        public async Task<ActionResult> AddUserWithRolesAsync(RegistrationModel model)
        {
            var result = await _userService.AddUserWithRolesAsync(model);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(result.Success);
        }
    }
}
