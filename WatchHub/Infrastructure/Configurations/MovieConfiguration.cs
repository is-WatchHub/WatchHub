using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure;

internal class MovieConfiguration : IEntityTypeConfiguration<MoviesDomain.Movie>
{
    public void Configure(EntityTypeBuilder<MoviesDomain.Movie> builder)
    {
        builder
            .ToTable(MovieTableConstants.MOVIE_TABLE_NAME);
        
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName(MovieTableConstants.MOVIE_ID_COLUMN_NAME)
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnName(MovieTableConstants.MOVIE_TITLE_COLUMN_NAME)
            .HasMaxLength(MovieTableConstants.MOVIE_TITLE_COLUMN_MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasColumnName(MovieTableConstants.MOVIE_DESCRIPTION_COLUMN_NAME)
            .HasMaxLength(MovieTableConstants.MOVIE_DESCRIPTION_COLUMN_MAX_LENGTH)
            .IsRequired();

        builder
            .Property(x => x.Genre)
            .HasColumnName(MovieTableConstants.MOVIE_GENRE_COLUMN_NAME)
            .HasConversion<string>()
            .IsRequired();

        builder
            .OwnsOne(x => x.Media, optionsBuilder =>
        {
            optionsBuilder
                .Property(x => x.ContentUrl)
                .HasColumnName(MovieTableConstants.MOVIE_CONTENT_URL_COLUMN_NAME)
                .HasMaxLength(MovieTableConstants.MOVIE_CONTENT_URL_COLUMN_MAX_LENGTH)
                .IsRequired();
            
            optionsBuilder
                .Property(x => x.CoverImageUrl)
                .HasColumnName(MovieTableConstants.MOVIE_COVER_IMAGE_URL_COLUMN_NAME)
                .HasMaxLength(MovieTableConstants.MOVIE_COVER_IMAGE_URL_COLUMN_MAX_LENGTH)
                .IsRequired();
        });
    }
}

