using Infrastructure.Constants;
using IntegrationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder
            .ToTable(PlatformTableConstants.PLATFORM_TABLE_NAME);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName(PlatformTableConstants.PLATFORM_ID_COLUMN_NAME)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName(PlatformTableConstants.PLATFORM_NAME_COLUMN_NAME)
            .HasMaxLength(PlatformTableConstants.PLATFORM_NAME_COLUMN_MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.Url)
            .HasColumnName(PlatformTableConstants.PLATFORM_URL_COLUMN_NAME)
            .HasMaxLength(PlatformTableConstants.PLATFORM_URL_COLUMN_MAX_LENGTH)
            .IsRequired();
    }
}

