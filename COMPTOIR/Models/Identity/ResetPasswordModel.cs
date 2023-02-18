using System.ComponentModel.DataAnnotations;

namespace COMPTOIR.Models.Identity
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
