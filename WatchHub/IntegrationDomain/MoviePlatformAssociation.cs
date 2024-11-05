namespace IntegrationDomain;

public class MoviePlatformAssociation
{
    public Guid Id { get; set; }
    public string ExternalPlatformId { get; set; }
    public virtual Platform Platform { get; set; }
}