namespace Infrastructure.Models;

public class MoviePlatformAssociationModel
{
    public Guid Id { get; set; }
    public Guid IntegrationId { get; set; }
    public Guid PlatformId { get; set; }
    public string ExternalPlatformId { get; set; }
    public virtual PlatformModel Platform { get; set; }
    public virtual IntegrationModel Integration { get; set; }
}