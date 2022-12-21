namespace COMPTOIR.Models.Identity
{
    public class ChangePasswordModel
    {
        public string? UserName { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
