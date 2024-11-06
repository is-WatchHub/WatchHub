namespace MoviesApplication.Dtos.Outgoing;

public class MovieInfoResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string CoverImageUrl { get; set; }
}