using Moq;
using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outcoming;
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
    public async Task GetAsync_ShouldReturnListOfMovies()
    {
        // Arrange
        var movies = new List<Movie> { new Movie { Id = Guid.NewGuid(), Title = "Test Movie" } };
        _movieRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(movies);

        _moviesMapperMock
            .Setup(mapper => mapper.Map<Movie, MovieInfoResponseDto>(It.IsAny<Movie>()))
            .Returns(new MovieInfoResponseDto { Id = Guid.NewGuid(), Title = "Test Movie" });

        // Act
        var result = await _moviesService.GetAsync();

        // Assert
        Assert.NotEmpty(result);
        _movieRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnMovie_WhenMovieExists()
    {
        // Arrange
        var movie = new Movie { Id = Guid.NewGuid(), Title = "Test Movie" };
        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movie.Id)).ReturnsAsync(movie);

        _moviesMapperMock
            .Setup(mapper => mapper.Map<Movie, FullResponseMovieDto>(movie))
            .Returns(new FullResponseMovieDto { Id = movie.Id, Title = movie.Title });

        // Act
        var result = await _moviesService.GetByIdAsync(movie.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(movie.Title, result.Title);
        _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(movie.Id), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldAddMovieAndReturnCreatedMovie()
    {
        // Arrange
        var createMovieDto = new CreateMovieDto { Title = "New Movie", Description = "Test Description" };
        var movie = new Movie { Id = Guid.NewGuid(), Title = createMovieDto.Title };
        
        _moviesMapperMock.Setup(mapper => mapper.Map<CreateMovieDto, Movie>(createMovieDto)).Returns(movie);
        _movieRepositoryMock.Setup(repo => repo.CreateAsync(movie)).ReturnsAsync(movie);
        _moviesMapperMock.Setup(mapper => mapper.Map<Movie, FullResponseMovieDto>(movie))
                         .Returns(new FullResponseMovieDto { Id = movie.Id, Title = movie.Title });

        // Act
        var result = await _moviesService.AddAsync(createMovieDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(movie.Title, result.Title);
        _movieRepositoryMock.Verify(repo => repo.CreateAsync(movie), Times.Once);
    }
    
    [Fact]
    public async Task GetInfoByIdAsync_ShouldReturnMovieInfo_WhenMovieExists()
    {
        // Arrange
        var movieId = Guid.NewGuid();
        var movie = new Movie
        {
            Id = movieId,
            Description = "Test Description",
            Media = new MovieMedia { ContentUrl = "https://content-url.com" }
        };

        var expectedResponse = new AdditionalMovieInfoResponseDto
        {
            Id = movieId,
            Description = movie.Description,
            ContentUrl = movie.Media.ContentUrl
        };

        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId)).ReturnsAsync(movie);
        _moviesMapperMock.Setup(mapper => mapper.Map<Movie, AdditionalMovieInfoResponseDto>(movie)).Returns(expectedResponse);

        // Act
        var result = await _moviesService.GetInfoByIdAsync(movieId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Id, result.Id);
        Assert.Equal(expectedResponse.Description, result.Description);
        Assert.Equal(expectedResponse.ContentUrl, result.ContentUrl);

        _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(movieId), Times.Once);
        _moviesMapperMock.Verify(mapper => mapper.Map<Movie, AdditionalMovieInfoResponseDto>(movie), Times.Once);
    }

    [Fact]
    public async Task GetInfoByIdAsync_ShouldReturnNull_WhenMovieDoesNotExist()
    {
        // Arrange
        var movieId = Guid.NewGuid();

        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId)).ReturnsAsync((Movie)null);

        // Act
        var result = await _moviesService.GetInfoByIdAsync(movieId);

        // Assert
        Assert.Null(result);
        _movieRepositoryMock.Verify(repo => repo.GetByIdAsync(movieId), Times.Once);
        _moviesMapperMock.Verify(mapper => mapper.Map<Movie, AdditionalMovieInfoResponseDto>(It.IsAny<Movie>()), Times.Once);
    }

    
     [Fact]
    public async Task GetByFilterAsync_ShouldReturnMoviesByGenre_WhenGenreIsSpecified()
    {
        // Arrange
        var genre = "Comedy";
        var filterDto = new MovieFilterDto { Genre = genre };

        var movies = new List<Movie>
        {
            new Movie { Id = Guid.NewGuid(), Title = "Comedy Movie", Genre = Genre.Comedy }
        };
        _movieRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(movies);

        _moviesMapperMock
            .Setup(mapper => mapper.Map<Movie, MovieInfoResponseDto>(It.IsAny<Movie>()))
            .Returns(new MovieInfoResponseDto { Id = movies.First().Id, Title = movies.First().Title, Genre = movies.First().Genre.ToString()});

        // Act
        var result = await _moviesService.GetByFilterAsync(filterDto);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, movie => Assert.Equal(genre, movie.Genre));
    }
}