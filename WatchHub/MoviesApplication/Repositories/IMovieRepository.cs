using MoviesDomain;

public interface IMovieRepository
{
    Task<Movie> GetByIdAsync(Guid id);
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie> CreateAsync(Movie movie);
}