using Infrastructure.Exceptions;
using IntegrationApplication;
using IntegrationDomain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class IntegrationRepository : IIntegrationRepository
{
    private readonly ApplicationDbContext _context;

    public IntegrationRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Integration> GetByMovieIdAsync(Guid id)
    {
        var integration = await _context.Integrations.FirstOrDefaultAsync(x => x.MovieId == id);
        if (integration is null)
        {
            throw new NotFoundException($"Integration with this movie id {id} does not exist!");
        }

        return integration;
    }
}
