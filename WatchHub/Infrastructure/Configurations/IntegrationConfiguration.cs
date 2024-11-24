using Infrastructure.Constants;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class IntegrationConfiguration : IEntityTypeConfiguration<IntegrationModel>
{ 
    public void Configure(EntityTypeBuilder<IntegrationModel> builder)
    {
        builder
            .ToTable(IntegrationTableConstants.INTEGRATION_TABLE_NAME);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName(IntegrationTableConstants.INTEGRATION_ID_COLUMN_NAME)
            .IsRequired();

        builder
            .Property(x => x.MovieId)
            .HasColumnName(IntegrationTableConstants.INTEGRATION_MOVIE_ID_COLUMN_NAME)
            .IsRequired();

        builder
            .HasMany(x => x.Association)
            .WithOne(x => x.Integration)
            .HasForeignKey(x => x.IntegrationId)
            .IsRequired();
    }
}
