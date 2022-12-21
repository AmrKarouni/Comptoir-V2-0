using Microsoft.AspNetCore.Identity;

namespace COMPTOIR.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsPasswordChanged { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
