using MoviesDomain;

namespace MoviesApplication.Dtos.Outgoing;

public class ResponseMovieDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Genre Genre { get; set; }
    public MovieMedia Media { get; set; }
}