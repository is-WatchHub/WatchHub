using Infrastructure.Constants;
using IntegrationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class MoviePlatformAssociationConfiguration : IEntityTypeConfiguration<MoviePlatformAssociation>
{
    public void Configure(EntityTypeBuilder<MoviePlatformAssociation> builder)
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
            .HasOne(x => x.Platform)
            .WithOne()
            .HasForeignKey("movie_platform_association_id")
            .IsRequired();
    }
}
