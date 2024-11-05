using Infrastructure.Mappers;
using Infrastructure.MappingProfiles;
using IntegrationApplication.Mappers;
using MoviesApplication.Mappers;
using UserManagementApplication.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(
    typeof(UserManagementMappingProfile),
    typeof(MoviesMappingProfile),
    typeof(IntegrationMappingProfile)
);

builder.Services.AddScoped<IUserManagementMapper, UserManagementMapper>();
builder.Services.AddScoped<IMoviesMapper, MoviesMapper>();
builder.Services.AddScoped<IIntegrationMapper, IntegrationMapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();