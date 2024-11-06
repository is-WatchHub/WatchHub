using MoviesDomain;

namespace MoviesApplication.Dtos.Incoming;

public class CreateMovieDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string ContentUrl { get; set; }
    public string CoverImageUrl { get; set; }
}