using COMPTOIR.Constants;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Identity;
using COMPTOIR.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace COMPTOIR.Contexts
{
    public class ApplicationDbContextSeed
    {
        public async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager,
                                              RoleManager<IdentityRole> roleManager,
                                              IPlaceService placeService)
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

            //Seed Places & Categories
            await placeService.PostPlaceCategoryAsync(new PlaceCategory
            {
                Id = 0,
                Name = "Production Category",
                IsCook = true,
                IsAmountIgnored = true,
                IsSend = true,
                IsReceive = true,
                IsDeleted = false
            });

            await placeService.PostPlaceCategoryAsync(new PlaceCategory
            {
                Id = 0,
                Name = "Pos Category",
                IsCook = false,
                IsAmountIgnored = true,
                IsSend = true,
                IsReceive = true,
                IsDeleted = false
            });

            await placeService.PostPlaceCategoryAsync(new PlaceCategory
            {
                Id = 0,
                Name = "Client Category",
                IsCook = false,
                IsAmountIgnored = true,
                IsSend = false,
                IsReceive = true,
                IsDeleted = false
            });

            await placeService.PostPlaceAsync(new Place
            {
                Id = 0,
                Name = "Production",
                Address = "",
                PhoneNumber = "",
                IpAddress = "",
                LogoUrl = "",
                CategoryId = 1,
                IsDeleted = false,
            });

            await placeService.PostPlaceAsync(new Place
            {
                Id = 0,
                Name = "Pos",
                Address = "",
                PhoneNumber = "",
                IpAddress = "",
                LogoUrl = "",
                CategoryId = 2,
                IsDeleted = false,
            });

            await placeService.PostPlaceAsync(new Place
            {
                Id = 0,
                Name = "Client",
                Address = "",
                PhoneNumber = "",
                IpAddress = "",
                LogoUrl = "",
                CategoryId = 3,
                IsDeleted = false,
            });
        }
    }
}
