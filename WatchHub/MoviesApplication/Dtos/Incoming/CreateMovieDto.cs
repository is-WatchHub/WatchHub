using MoviesDomain;

namespace MoviesApplication.Dtos.Incoming;

public class CreateMovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Genre Genre { get; set; }
    public MovieMedia Media { get; set; }
}