using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using MoviesDomain;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Movie> CreateAsync(Movie movie)
    {
        var result = await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie> GetByIdAsync(Guid id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null)
        {
            throw new NotFoundException($"Movie with this id {id} does not exist!");
        }

        return movie;
    }
}
