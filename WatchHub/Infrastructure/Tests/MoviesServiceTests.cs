using Moq;
using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outgoing;
using MoviesApplication.Mappers;
using MoviesApplication.Services;
using MoviesDomain;
using Xunit;

namespace Infrastructure.Tests;

public class MoviesServiceTests
{
    private readonly Mock<IMovieRepository> _movieRepositoryMock;
    private readonly Mock<IMoviesMapper> _moviesMapperMock;
    private readonly MoviesService _moviesService;

    public MoviesServiceTests()
    {
        _movieRepositoryMock = new Mock<IMovieRepository>();
        _moviesMapperMock = new Mock<IMoviesMapper>();
        _moviesService = new MoviesService(_movieRepositoryMock.Object, _moviesMapperMock.Object);
    }

    [Fact]
    public async Task GetMoviesAsync_ShouldReturnListOfMovies()
    {
        // Arrange
        var movies = new List<Movie> { new Movie { Id = Guid.NewGuid(), Title = "Test Movie" } };
        _movieRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(movies);

        _moviesMapperMock
            .Setup(mapper => mapper.Map<Movie, MovieInfoResponseDto>(It.IsAny<Movie>()))
            .Returns(new MovieInfoResponseDto { Id = Guid.NewGuid(), Title = "Test Movie" });

        // Act
        var result = await _moviesService.GetMoviesAsync();

        // Assert
        Assert.NotEmpty(result);
        _movieRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetMovieByIdAsync_ShouldReturnMovie_WhenMovieExists()
    {
        // Arrange
        var movie = new Movie { Id = Guid.NewGuid(), Title = "Test Movie" };
        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movie.Id)).ReturnsAsync(movie);

        _moviesMapperMock
            .Setup(mapper => mapper.Map<Movie, FullResponseMovieDto>(movie))
            .Returns(new FullResponseMovieDto { Id = movie.Id, Title = movie.Title });

        // Act
        var result = await _moviesService.GetMovieByIdAsync(movie.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(movie.Title, result.Title);
        _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(movie.Id), Times.Once);
    }

    [Fact]
    public async Task AddMovieAsync_ShouldAddMovieAndReturnCreatedMovie()
    {
        // Arrange
        var createMovieDto = new CreateMovieDto { Title = "New Movie", Description = "Test Description" };
        var movie = new Movie { Id = Guid.NewGuid(), Title = createMovieDto.Title };
        
        _moviesMapperMock.Setup(mapper => mapper.Map<CreateMovieDto, Movie>(createMovieDto)).Returns(movie);
        _movieRepositoryMock.Setup(repo => repo.CreateAsync(movie)).ReturnsAsync(movie);
        _moviesMapperMock.Setup(mapper => mapper.Map<Movie, FullResponseMovieDto>(movie))
                         .Returns(new FullResponseMovieDto { Id = movie.Id, Title = movie.Title });

        // Act
        var result = await _moviesService.AddMovieAsync(createMovieDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(movie.Title, result.Title);
        _movieRepositoryMock.Verify(repo => repo.CreateAsync(movie), Times.Once);
    }
}