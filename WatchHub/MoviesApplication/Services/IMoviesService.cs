using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outgoing;

namespace MoviesApplication.Services;

public interface IMoviesService
{
    Task<IEnumerable<MovieInfoResponseDto>> GetMoviesAsync();
    Task<FullResponseMovieDto> GetMovieByIdAsync(Guid id);
    Task<AdditionalMovieInfoResponseDto> GetMovieInfoByIdAsync(Guid id);
    Task<FullResponseMovieDto> AddMovieAsync(CreateMovieDto createMovieDto);
    Task<IEnumerable<MovieInfoResponseDto>> GetMoviesByCriteriaAsync(MovieFilterDto filterDto);
    Task<FullResponseMovieDto> GetRandomMovieByGenreAsync(RandomMovieByGenreDto genreDto);
}