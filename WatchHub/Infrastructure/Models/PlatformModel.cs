namespace Infrastructure.Models;

public class PlatformModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public virtual IEnumerable<MoviePlatformAssociationModel> Associations { get; set; }
}