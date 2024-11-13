using IntegrationApplication.Dtos;

namespace IntegrationApplication.Services;

public interface IIntegrationService
{
    Task<MovieInformationDto> GetMovieInformationByMovieIdAsync(Guid id);
}