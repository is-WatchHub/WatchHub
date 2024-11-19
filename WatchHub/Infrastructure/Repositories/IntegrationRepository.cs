using Infrastructure.Exceptions;
using Infrastructure.Models;
using IntegrationApplication;
using IntegrationApplication.Mappers;
using IntegrationApplication.Repositories;
using IntegrationDomain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IntegrationRepository : IIntegrationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IIntegrationMapper _mapper;

    public IntegrationRepository(ApplicationDbContext context, IIntegrationMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Integration> GetByMovieIdAsync(Guid id)
    {
        var model = await _context.Integrations
            .Include(x => x.Association)
                .ThenInclude(association => association.Platform)
            .FirstOrDefaultAsync(x => x.MovieId == id) ?? 
                          throw new NotFoundException($"Integration with this movie id {id} does not exist!");

        var integration = _mapper.Map<IntegrationModel, Integration>(model);

        return integration;
    }
}
