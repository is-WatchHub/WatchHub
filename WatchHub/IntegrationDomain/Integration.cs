namespace IntegrationDomain;

public class Integration
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }
    public virtual ICollection<MoviePlatform> Platforms { get; set; } = new List<MoviePlatform>();
}