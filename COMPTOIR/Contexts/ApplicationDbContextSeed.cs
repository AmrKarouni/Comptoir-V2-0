using COMPTOIR.Constants;
using COMPTOIR.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace COMPTOIR.Contexts
{
    public class ApplicationDbContextSeed
    {
        public async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles

            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.SuperUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.InventoryAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.InventoryUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.POSAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.POSUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.ProductionAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.ProductionUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
            
            //Seed Default User
            var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true, IsPasswordChanged = true,IsActive = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
    }
}
