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
                                              IDiscountService discountService,
                                              ICustomerService customerService,
                                              IPaymentChannelService paymentChannelService,
                                              IPaymentMethodService paymentMethodService,
                                              IUnitService unitService,
                                              ITaxService taxService,
                                              IPlaceService placeService
                                              )
        {
            //Seed Roles

            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.SuperUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = Authorization.default_username,
                Email = Authorization.default_email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsPasswordChanged = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }

            //Seed Discounts
            await discountService.PostDiscountAsync(new Discount
            {
                Id = 0,
                Name = "05 %",
                Value = 0.05
            });
            await discountService.PostDiscountAsync(new Discount
            {
                Id = 0,
                Name = "10 %",
                Value = 0.1
            });
            await discountService.PostDiscountAsync(new Discount
            {
                Id = 0,
                Name = "15 %",
                Value = 0.15
            });
            await discountService.PostDiscountAsync(new Discount
            {
                Id = 0,
                Name = "20 %",
                Value = 0.2
            });
            await discountService.PostDiscountAsync(new Discount
            {
                Id = 0,
                Name = "25 %",
                Value = 0.25
            });

            //Seed Customer
            await customerService.PostCustomerAsync(new Customer
            {
                Id = 0,
                Name = "Guest",
                ContactNumber01 = null,
                ContactNumber02 = null,
                ContactNumber03 = null,
                Gender = null,
                LoyalityLevel = null,
                Address01 = null,
                Address02 = null,
                Address03 = null
            });

            //Seed Payment Channels
            await paymentChannelService.PostPaymentChannelsync(new PaymentChannel
            {
                Id = 0,
                Name = "Takeaway",
                Description = "Takeaway"
            });
            await paymentChannelService.PostPaymentChannelsync(new PaymentChannel
            {
                Id = 0,
                Name = "Delivery",
                Description = "Delivery"
            });

            //Seed Payment Methods
            await paymentMethodService.PostPaymentMethodsync(new PaymentMethod
            {
                Id = 0,
                Name = "Cash",
                Description = "Cash"
            });
            await paymentMethodService.PostPaymentMethodsync(new PaymentMethod
            {
                Id = 0,
                Name = "Card",
                Description = "Card"
            });
            await paymentMethodService.PostPaymentMethodsync(new PaymentMethod
            {
                Id = 0,
                Name = "Gift Card",
                Description = "Gift Card"
            });

            //Seed Units
            await unitService.PostUnitAsync(new Unit
            {
                Id = "G",
                Name = "Gram",
                Description = "Gram"
            });
            await unitService.PostUnitAsync(new Unit
            {
                Id = "PC",
                Name = "Piece",
                Description = "Piece"
            });

            //Seed Tax
            await taxService.PostTaxAsync(new Tax
            {
                Id = 0,
                Name = "VAT",
                Rate = 0.05,
            });

            //Seed POS Place
            await placeService.PostPlaceAsync(new Place
            {
                Id = 0,
                Name = "POS",
                LocationAddress = null,
                Lat = null,
                Lon = null,
                PhoneNumberOne = null,
                PhoneNumbertwo = null,
                PhoneNumberThree = null,
                EmailAddress = null,
                IpAddress = null,
                ImageUrl = null,
                ThumbUrl = null,
                AverageRentalFees = 0,
                IsPos = true,
                IsManufacturer = true
            });
        }
    }
}
