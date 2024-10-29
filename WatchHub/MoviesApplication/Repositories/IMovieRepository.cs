using MoviesDomain;

public interface IMovieRepository
{
    public Movie GetMovieByID(Guid id);
    public IList<Movie> GetAllMovies();
    public Movie GetRandomMovie();
    public Movie CreateMovie(Movie movie);
}