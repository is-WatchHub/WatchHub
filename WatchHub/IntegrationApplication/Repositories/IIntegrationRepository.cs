using IntegrationDomain;

namespace IntegrationApplication.Repositories;

public interface IIntegrationRepository
{
    Task<Integration> GetByMovieIdAsync(Guid id);
}