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
        _requestHandler = BuildHandlerChain(handlers ?? throw new ArgumentNullException(nameof(handlers)));
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

    private IRequestHandler BuildHandlerChain(IEnumerable<IRequestHandler> handlers)
    {
        var handlerList = handlers.ToList();
        
        if (handlerList is null || handlerList.Count == 0)
            throw new InvalidOperationException("No request handlers provided.");

        for (int i = 0; i < handlerList.Count - 1; i++)
            handlerList[i].SetNext(handlerList[i + 1]);

        return handlerList.First();
    }
}