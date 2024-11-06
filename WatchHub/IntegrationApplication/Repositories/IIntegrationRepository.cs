using IntegrationDomain;

namespace IntegrationApplication;

public interface IIntegrationRepository
{
    Task<Integration> GetByMovieIdAsync(Guid id);
}