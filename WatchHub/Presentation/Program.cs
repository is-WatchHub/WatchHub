using System.Net;
using System.Text.Json;
using Infrastructure;
using Infrastructure.Handlers;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpLogging;
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

var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

if (allowedOrigins is null || allowedOrigins.Length == 0)
    throw new ArgumentNullException(nameof(allowedOrigins),
        "CorsSettings:AllowedOrigins is not configured or is empty.");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", cors =>
    {
        cors
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
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

var kinopoiskApiKey = builder.Configuration["ServicesApiKeys:Kinopoisk"];
if (string.IsNullOrEmpty(kinopoiskApiKey))
    throw new ArgumentNullException(nameof(kinopoiskApiKey), "Kinopoisk API key cannot be null or empty.");

var kinopoiskHandlerName = builder.Configuration["HandlerNames:Kinopoisk"];
if (string.IsNullOrEmpty(kinopoiskHandlerName))
    throw new ArgumentNullException(nameof(kinopoiskHandlerName), "Kinopoisk handler name cannot be null or empty.");

builder.Services.AddScoped<IRequestHandler>(_ => 
    new KinopoiskHandler(kinopoiskApiKey, kinopoiskHandlerName));

var omdbApiKey = builder.Configuration["ServicesApiKeys:Omdb"];
if (string.IsNullOrEmpty(omdbApiKey))
    throw new ArgumentNullException(nameof(omdbApiKey), "OMDb API key cannot be null or empty.");

var omdbHandlerName = builder.Configuration["HandlerNames:Omdb"];
if (string.IsNullOrEmpty(omdbHandlerName))
    throw new ArgumentNullException(nameof(omdbHandlerName), "OMDb handler name cannot be null or empty.");

builder.Services.AddScoped<IRequestHandler>(_ =>
    new OmdbHandler(omdbApiKey, omdbHandlerName));

builder.Services.AddScoped<IIntegrationMapper, IntegrationMapper>();

builder.Services.AddScoped<IIntegrationRepository, IntegrationRepository>();

builder.Services.AddScoped<IIntegrationService, IntegrationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserManagementService, UserManagementService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096;
    options.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseHttpLogging();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature is not null)
        {
            var errorResponse = new Dictionary<string, object>
            {
                { "StatusCode", context.Response.StatusCode },
                { "Message", exceptionHandlerPathFeature.Error.Message }
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