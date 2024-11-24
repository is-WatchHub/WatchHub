using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outcoming;
using MoviesApplication.Mappers;
using MoviesDomain;

namespace MoviesApplication.Services;

public class MoviesService : IMoviesService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMoviesMapper _moviesMapper;

    public MoviesService(IMovieRepository movieRepository, IMoviesMapper moviesMapper)
    {
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        _moviesMapper = moviesMapper ?? throw new ArgumentNullException(nameof(moviesMapper));
    }

    public async Task<IEnumerable<MovieInfoResponseDto>> GetAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        return movies.Select(movie => _moviesMapper.Map<Movie, MovieInfoResponseDto>(movie));
    }

    public async Task<FullResponseMovieDto> GetByIdAsync(Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return _moviesMapper.Map<Movie, FullResponseMovieDto>(movie);
    }

    public async Task<AdditionalMovieInfoResponseDto> GetInfoByIdAsync(Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return _moviesMapper.Map<Movie, AdditionalMovieInfoResponseDto>(movie);
    }

    public async Task<FullResponseMovieDto> AddAsync(CreateMovieDto createMovieDto)
    {
        var movie = _moviesMapper.Map<CreateMovieDto, Movie>(createMovieDto);
        var createdMovie = await _movieRepository.CreateAsync(movie);
        return _moviesMapper.Map<Movie, FullResponseMovieDto>(createdMovie);
    }

    public async Task<IEnumerable<MovieInfoResponseDto>> GetByFilterAsync(MovieFilterDto filterDto)
    {
        IEnumerable<Movie> movies;
        if (filterDto.Genre is not null && Enum.TryParse<Genre>(filterDto.Genre, true, out var genreEnum))
        {
            movies = (await _movieRepository.GetAllAsync()).Where(m => m.Genre == genreEnum);
        }
        else
        {
            movies = await _movieRepository.GetAllAsync();
        }

        return movies.Select(movie => _moviesMapper.Map<Movie, MovieInfoResponseDto>(movie));
    }
}