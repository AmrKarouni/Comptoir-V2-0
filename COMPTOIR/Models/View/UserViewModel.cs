namespace COMPTOIR.Models.View
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool? IsActive { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
