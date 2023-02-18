using COMPTOIR.Contexts;
using COMPTOIR.Models.Identity;
using COMPTOIR.Services;
using COMPTOIR.Services.Interfaces;
using COMPTOIR.Settings;
using Email.Service.Models;
using Email.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//var testConnectionString = "MochaConnection";
var localConnectionString = "LocalConnection";
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString(localConnectionString);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));



//Add Configuration For Required Email 
builder.Services.Configure<IdentityOptions>(
    opts => opts.SignIn.RequireConfirmedEmail = true);


builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPosService, PosService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddTransient<ApplicationDbContextSeed>();
builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials();
}));


//Add Email Configurations
var emailconfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailconfig);


// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    await SeedData(app);

async Task SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory?.CreateScope())
    {
        var service = scope?.ServiceProvider.GetService<ApplicationDbContextSeed>();
        var userManager = scope?.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope?.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await service.SeedEssentialsAsync(userManager, roleManager);
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "./App_Media/products")),
    RequestPath = "/products"

});

app.UseAuthorization();

app.MapControllers();

app.Run();
