using System.Net;
using System.Text.Json;
using Infrastructure;
using Infrastructure.Handlers;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MoviesApplication.Services;

using Infrastructure.Mappers;
using Infrastructure.MappingProfiles;
using Infrastructure.Repositories;
using IntegrationApplication;
using IntegrationApplication.Handlers;
using IntegrationApplication.Mappers;
using IntegrationApplication.Services;
using MoviesApplication.Mappers;
using UserManagementApplication.Mappers;
using UserManagementApplication.Repositories;
using UserManagementApplication.Services;

var builder = WebApplication.CreateBuilder(args);

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(defaultConnection))
{
    throw new ArgumentNullException(nameof(defaultConnection), "Connection string cannot be null or empty.");
}

var allowedHosts = builder.Configuration.GetSection("AllowedHosts").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", cors =>
    {
        cors
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(defaultConnection);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddAutoMapper(
    typeof(UserManagementMappingProfile),
    typeof(MoviesMappingProfile),
    typeof(IntegrationMappingProfile)
);

builder.Services.AddSingleton<IUserManagementMapper, UserManagementMapper>();
builder.Services.AddSingleton<IMoviesMapper, MoviesMapper>();
builder.Services.AddSingleton<IIntegrationMapper, IntegrationMapper>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

builder.Services.AddScoped<IRequestHandler>(sp => 
    new KinopoiskHandler(builder.Configuration["ServicesApiKeys:Kinopoisk"], 
        builder.Configuration["HandlerNames:Kinopoisk"]));

builder.Services.AddScoped<IRequestHandler>(sp =>
    new OmdbHandler(builder.Configuration["ServicesApiKeys:Omdb"],
        builder.Configuration["HandlerNames:Omdb"]));

builder.Services.AddScoped<IIntegrationMapper, IntegrationMapper>();

builder.Services.AddScoped<IIntegrationRepository, IntegrationRepository>();

builder.Services.AddScoped<IIntegrationService, IntegrationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserManagementService, UserManagementService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("CorsPolicy");

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

app.MapControllers();
app.MapIdentityApi<ApplicationUser>();

app.Run();