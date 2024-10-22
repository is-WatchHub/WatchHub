using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementApplication.Dtos;

namespace UserManagementApplication;

public class UserDbContext  : IdentityDbContext<ApplicationUser>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Port=5431;Database=watch_hub;Username=test_user;Password=HailTheKing")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors(true);
        }
    }
}