using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThriftStore.Business.CategoryModule.Concrete;
using ThriftStore.Business.CategoryModule.Interface;
using ThriftStore.Business.UserModule.Concrete;
using ThriftStore.Business.UserModule.Interface;
using ThriftStore.Data;
using ThriftStore.Data.Domain.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Add ASP.NET Core Identity services
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Configure Identity options
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
})
    .AddEntityFrameworkStores<StoreDbContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserAccountService, UserAccountService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IAuthUser, AuthUser>();
builder.Services.AddTransient<ILogger<ItemService>, Logger<ItemService>>();
builder.Services.AddTransient<ILogger<UserAccountService>, Logger<UserAccountService>>();

// Add UserManager<ApplicationUser>, RoleManager<ApplicationRole>, and SignInManager<ApplicationUser>
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
