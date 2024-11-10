using Infrastructure.Constants;
using IntegrationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations;

internal class IntegrationConfiguration : IEntityTypeConfiguration<Integration>
{
    public void Configure(EntityTypeBuilder<Integration> builder)
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
            .HasMany(x => x.Platforms)
            .WithOne()
            .HasForeignKey("integration_id")
            .IsRequired();
    }
}
