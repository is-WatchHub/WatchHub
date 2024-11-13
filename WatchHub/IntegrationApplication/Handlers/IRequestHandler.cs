using IntegrationApplication.Dtos;
using IntegrationDomain;

namespace IntegrationApplication.Handlers;

public interface IRequestHandler
{
    IRequestHandler SetNext(IRequestHandler handler);
    Task CollectMovieInformation(Integration integration, MovieInformationDto movieInformationDto);
}