using System.Net;
using System.Text.Json;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Infrastructure.Mappers;
using Infrastructure.MappingProfiles;
using IntegrationApplication.Mappers;
using MoviesApplication.Mappers;
using UserManagementApplication.Mappers;

var builder = WebApplication.CreateBuilder(args);

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(defaultConnection))
{
    throw new ArgumentNullException(nameof(defaultConnection), "Connection string cannot be null or empty.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(defaultConnection);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(
    typeof(UserManagementMappingProfile),
    typeof(MoviesMappingProfile),
    typeof(IntegrationMappingProfile)
);

builder.Services.AddSingleton<IUserManagementMapper, UserManagementMapper>();
builder.Services.AddSingleton<IMoviesMapper, MoviesMapper>();
builder.Services.AddSingleton<IIntegrationMapper, IntegrationMapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature != null)
        {
            var errorResponse = new Dictionary<string, object>
            {
                { "StatusCode", context.Response.StatusCode },
                { "Message", "An unexpected error occurred." }
            };
            
            if (app.Environment.IsDevelopment())
            {
                errorResponse["Detailed"] = exceptionHandlerPathFeature.Error.Message;
            }

            var errorJson = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(errorJson);
        }
    });
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();