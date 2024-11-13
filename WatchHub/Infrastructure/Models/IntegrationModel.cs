namespace Infrastructure.Models;

public class IntegrationModel
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }
    public virtual ICollection<MoviePlatformAssociationModel> Association { get; set; }
}