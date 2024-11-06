using IntegrationDomain;

public interface IIntegrationRepository
{
    Task<Integration> GetByMovieIdAsync(Guid id);
}