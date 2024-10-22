namespace MoviesDomain;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Genre Genre { get; set; }
    public double Rating { get; set; }
    public string CoverImageUrl { get; set; }
    public VideoPlayer Player { get; set; }
}