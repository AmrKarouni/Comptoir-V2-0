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
                                              IPlaceService placeService,
                                              ITransactionService transactionService,
                                              IChannelService channelService,
                                              ICustomerService customerService)
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

            //Seed Transaction Categories
            await transactionService.PostTransactionCategoryAsync(new TransactionCategory
            {
                Name = "Production",
                IsViceversa = false,
                IsPayment = false,
                InOut = null,
                IsPos = false,
                Description = null,
                IsDeleted =false
            });
            await transactionService.PostTransactionCategoryAsync(new TransactionCategory
            {
                Name = "Sale",
                IsViceversa = false,
                IsPayment = false,
                InOut = null,
                IsPos = true,
                Description = null,
                IsDeleted = false
            });
            await transactionService.PostTransactionCategoryAsync(new TransactionCategory
            {
                Name = "Transfer",
                IsViceversa = false,
                IsPayment = false,
                InOut = null,
                IsPos = false,
                Description = null,
                IsDeleted = false
            });

            //Seed Channel & Channel Category
            await channelService.PostChannelCategoryAsync(new ChannelCategory
            {
                Id = 0,
                Name = "Pos Channel Category",
                PlaceId =2,
                IsMiniPos = false,
                IsDeleted = false
            });

            await channelService.PostChannelAsync(new Channel
            {
                Id = 0,
                Name = "Pos Channel",
                CategoryId = 1,
                PositionX = null,
                PositionY = null,
                Rotation = null,
                Seats = null,
                IsAnonymous = true,
                IsMultiple = true,
                IsDeleted = false
            });

            //Seed Customer 
            await customerService.PostCustomerAsync(new Customer
            {
                Id = 0,
                Name = "Guest",
                ContactNumber01 = null,
                ContactNumber02 = null,
                ContactNumber03 = null,
                ContactNumber04 = null,
                ContactNumber05 = null,
                Gender = null,
                LoyalityLevel = null,
                Addresses01 = null,
                Addresses02 = null,
                Addresses03 = null,
                Addresses04 = null,
                Addresses05 = null,
                IsDeleted = false
            });

        }
    }
}
