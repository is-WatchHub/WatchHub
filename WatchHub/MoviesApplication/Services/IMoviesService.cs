using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outcoming;

namespace MoviesApplication.Services;

public interface IMoviesService
{
    Task<IEnumerable<MovieInfoResponseDto>> GetAsync();
    Task<FullResponseMovieDto> GetByIdAsync(Guid id);
    Task<AdditionalMovieInfoResponseDto> GetInfoByIdAsync(Guid id);
    Task<FullResponseMovieDto> AddAsync(CreateMovieDto createMovieDto);
    Task<IEnumerable<MovieInfoResponseDto>> GetByFilterAsync(MovieFilterDto filterDto);
}