using IntegrationApplication.Dtos;
using IntegrationApplication.Handlers;

namespace IntegrationApplication.Services;

public class IntegrationService : IIntegrationService
{
    private readonly IIntegrationRepository _repository;
    private IRequestHandler _requestHandler;

    public IntegrationService(IIntegrationRepository repository, IEnumerable<IRequestHandler> handlers)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Handle(handlers);
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

    public void Handle(IEnumerable<IRequestHandler> handlers)
    {
        IRequestHandler currentHandler = null;
        foreach (var handler in handlers)
        {
            if (currentHandler == null)
            {
                _requestHandler = handler;
                currentHandler = handler;
            }
            else
            {
                currentHandler = currentHandler.SetNext(handler);
            }
        }
    }
}