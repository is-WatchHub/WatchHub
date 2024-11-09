using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outgoing;
using MoviesApplication.Mappers;
using MoviesDomain;

namespace MoviesApplication.Services;

public class MoviesService : IMoviesService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMoviesMapper _moviesMapper;

    public MoviesService(IMovieRepository movieRepository, IMoviesMapper moviesMapper)
    {
        _movieRepository = movieRepository;
        _moviesMapper = moviesMapper;
    }

    public async Task<IEnumerable<MovieInfoResponseDto>> GetMoviesAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        return movies.Select(movie => _moviesMapper.Map<Movie, MovieInfoResponseDto>(movie));
    }

    public async Task<FullResponseMovieDto> GetMovieByIdAsync(Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return _moviesMapper.Map<Movie, FullResponseMovieDto>(movie);
    }

    public async Task<AdditionalMovieInfoResponseDto> GetMovieInfoByIdAsync(Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return _moviesMapper.Map<Movie, AdditionalMovieInfoResponseDto>(movie);
    }

    public async Task<FullResponseMovieDto> AddMovieAsync(CreateMovieDto createMovieDto)
    {
        var movie = _moviesMapper.Map<CreateMovieDto, Movie>(createMovieDto);
        var createdMovie = await _movieRepository.CreateAsync(movie);
        return _moviesMapper.Map<Movie, FullResponseMovieDto>(createdMovie);
    }

    public async Task<IEnumerable<MovieInfoResponseDto>> GetMoviesByCriteriaAsync(MovieFilterDto filterDto)
    {
        IEnumerable<Movie> movies;
        if (filterDto.Genre != null && Enum.TryParse<Genre>(filterDto.Genre, true, out var genreEnum))
        {
            movies = (await _movieRepository.GetAllAsync()).Where(m => m.Genre == genreEnum);
        }
        else
        {
            movies = await _movieRepository.GetAllAsync();
        }

        return movies.Select(movie => _moviesMapper.Map<Movie, MovieInfoResponseDto>(movie));
    }

    public async Task<FullResponseMovieDto> GetRandomMovieByGenreAsync(RandomMovieByGenreDto genreDto)
    {
        if (genreDto.Genre == null || !Enum.TryParse<Genre>(genreDto.Genre, true, out var genreEnum))
            throw new ArgumentException("Invalid genre specified");

        var movies = (await _movieRepository.GetAllAsync()).Where(m => m.Genre == genreEnum).ToList();

        if (!movies.Any())
            throw new InvalidOperationException("No movies found for the specified genre");

        var randomMovie = movies[new Random().Next(movies.Count)];
        return _moviesMapper.Map<Movie, FullResponseMovieDto>(randomMovie);

    }
}