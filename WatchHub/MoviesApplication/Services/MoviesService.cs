using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outgoing;
using MoviesApplication.Mappers;
using MoviesDomain;

namespace MoviesApplication.Services;

public class MoviesService
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

    public async Task<FullResponseMovieDto> AddMovieAsync(CreateMovieDto createMovieDto)
    {
        var movie = _moviesMapper.Map<CreateMovieDto, Movie>(createMovieDto);
        var createdMovie = await _movieRepository.CreateAsync(movie);
        return _moviesMapper.Map<Movie, FullResponseMovieDto>(createdMovie);
    }
}