using COMPTOIR.Models.AppModels;
using Microsoft.AspNetCore.Identity;

namespace COMPTOIR.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsPasswordChanged { get; set; }
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public string? ThumbUrl { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<Place>? Places { get; set; }
    }
}
