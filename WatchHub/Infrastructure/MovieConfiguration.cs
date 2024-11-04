using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain;

namespace Infrastructure
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable(Constants.MOVIE_TABLE_NAME);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(Constants.MOVIE_ID_COLUMN_NAME).IsRequired();
            builder.Property(x => x.Title).HasColumnName(Constants.MOVIE_TITLE_COLUMN_NAME).HasMaxLength(45).IsRequired();
            builder.Property(x => x.Description).HasColumnName(Constants.MOVIE_DESCRIPTION_COLUMN_NAME).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Genre).HasColumnName(Constants.MOVIE_GENRE_COLUMN_NAME).HasConversion<int>().IsRequired();
            builder.OwnsOne(x => x.Media, optionsBuilder =>
            {
                optionsBuilder.Property(x => x.ContentUrl).HasColumnName(Constants.MOVIE_CONTENT_URL_COLUMN_NAME).HasMaxLength(150).IsRequired();
                optionsBuilder.Property(x => x.CoverImageUrl).HasColumnName(Constants.MOVIE_COVER_IMAGE_URL_COLUMN_NAME).HasMaxLength(150).IsRequired();
            });
        }
    }
}
