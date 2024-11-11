using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using MoviesDomain;

namespace Infrastructure.Repositories;

internal class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
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
        if (movie == null)
        {
            throw new NotFoundException("Movie with this id does not exist!");
        }

        return movie;
    }

    public async Task<Movie> GetRandomAsync()
    {
        var movies = await _context.Movies.ToListAsync();
        if (!movies.Any())
        {
            throw new InvalidOperationException("Can't get random movie because movie table is empty!");
        }

        var randomIndex = new Random().Next(movies.Count);
        return movies[randomIndex];
    }
}
