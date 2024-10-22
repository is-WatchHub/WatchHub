using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementApplication;
using UserManagementApplication.Controllers;
using UserManagementApplication.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("UserManagementApplication")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(UserController).Assembly);

var app = builder.Build();

app.UseAuthentication(); // Включение аутентификации
app.UseAuthorization();  // Включение авторизации

app.MapDefaultControllerRoute();

app.Run();