namespace IntegrationApplication.Dtos;

public class RatingDto
{
    public double AggregatedValue { get; set; }
    public List<ServiceRatingDto> Ratings { get; set; }
}