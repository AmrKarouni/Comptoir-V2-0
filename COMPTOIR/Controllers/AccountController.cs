using COMPTOIR.Models.Identity;
using COMPTOIR.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMPTOIR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegistrationAsync(RegistrationModel model)
        {
            var result = await _userService.RegistrationAsync(model);
            if (result.Success == false)
            {
                return BadRequest(new { message = result.Message });
            }
            if (result == null)
            {
                return BadRequest(new { message = "Email Already Exist" });
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

        //[Authorize(Roles = "Admin, Developer")]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        //[Authorize(Roles = "Admin, Developer")]
        [HttpPost("deactivate-account")]
        public IActionResult DeactivateAccountAsync(string userId)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return BadRequest(new { message = "User Id is required" });
            }
            var response = _userService.DeactivateAccountAsync(userId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(new { message = "Bad Request" });
        }


        //[Authorize(Roles = "Admin, Developer")]
        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            if (string.IsNullOrEmpty(model.Id))
                return BadRequest(new { message = "Id is required" });
            var response = _userService.RevokeTokenById(model.Id);
            if (!response.Success)
                return NotFound(new { message = "User not found" });
            return Ok(response);
        }

        //[Authorize(Roles = "Admin, SuperUser")]
        [HttpPost("addnewrole/{roleName}")]
        public async Task<IActionResult> AddNewRole(string roleName)
        {
            return Ok(await _userService.AddNewRole(roleName));
        }
    }
}
