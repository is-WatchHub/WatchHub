using Infrastructure.Configurations;
using Infrastructure.Models;
using IntegrationDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesDomain;

namespace Infrastructure;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<MoviePlatformAssociation> PlatformAssociations { get; set; }

    public DbSet<Integration> Integrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new PlatformConfiguration());
        modelBuilder.ApplyConfiguration(new MoviePlatformAssociationConfiguration());
        modelBuilder.ApplyConfiguration(new IntegrationConfiguration());
    }
}
