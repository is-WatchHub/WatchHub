namespace MoviesApplication.Dtos.Outcoming;

public class FullResponseMovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string ContentUrl { get; set; }
    public string CoverImageUrl { get; set; }
}