using MoviesDomain;

public interface IMovieRepository
{
    Task<Movie> GetByIdAsync(Guid id);
    Task<IList<Movie>> GetAllAsync();
    Task<Movie> GetRandomAsync();
    Task<Movie> CreateAsync(Movie movie);
}