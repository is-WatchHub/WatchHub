using IntegrationApplication.Dtos;
using IntegrationApplication.Handlers;

namespace IntegrationApplication.Services;

public class IntegrationService : IIntegrationService
{
    private readonly IIntegrationRepository _repository;
    private readonly IRequestHandler _requestHandler;

    public IntegrationService(IIntegrationRepository repository, IRequestHandler requestHandler)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
    }

    public async Task<MovieInformationDto> GetMovieInformationByMovieIdAsync(Guid id)
    {
        var integration = await _repository.GetByMovieIdAsync(id);

        var movieInformation = new MovieInformationDto
        {
            Director = "",
            ActorNames = new List<string>(),
            Awards = "",
            Rating = new RatingDto
            {
                AggregatedValue = 0.0,
                Ratings = new List<ServiceRatingDto>()
            }
        };
        
        await _requestHandler.CollectMovieInformation(integration, movieInformation);

        return movieInformation;
    }
}