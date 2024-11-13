using Infrastructure.Constants;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class MoviePlatformAssociationConfiguration : IEntityTypeConfiguration<MoviePlatformAssociationModel>
{
    public void Configure(EntityTypeBuilder<MoviePlatformAssociationModel> builder)
    {
        builder
            .ToTable(MoviePlatformAssociationTableConstants.MOVIE_PLATFORM_ASSOCIATION_TABLE_NAME);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName(MoviePlatformAssociationTableConstants.MOVIE_PLATFORM_ASSOCIATION_ID_COLUMN_NAME)
            .IsRequired();

        builder
            .Property(x => x.ExternalPlatformId)
            .HasColumnName(MoviePlatformAssociationTableConstants.MOVIE_PLATFORM_ASSOCIATION_EXTERNAL_PLATFORM_ID_COLUMN_NAME)
            .HasMaxLength(MoviePlatformAssociationTableConstants.MOVIE_PLATFORM_ASSOCIATION_EXTERNAL_PLATFORM_ID_COLUMN_MAX_LENGTH)
            .IsRequired();

        builder
            .HasOne(x => x.Integration)
            .WithMany(x => x.Association)
            .HasForeignKey(x => x.IntegrationId)
            .IsRequired();

        builder
            .HasOne(x => x.Platform)
            .WithMany(x => x.Associations)
            .HasForeignKey(x => x.PlatformId)
            .IsRequired();
    }
}
