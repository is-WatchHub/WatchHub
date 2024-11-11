namespace IntegrationApplication.Dtos;

public class MovieInformationDto
{
    public string Director { get; set; }
    public RatingDto Rating { get; set; }
    public List<string> ActorNames { get; set; }
    public string Awards { get; set; }
}