using IntegrationApplication;
using IntegrationApplication.Dtos;
using IntegrationApplication.Handlers;
using IntegrationApplication.Services;
using IntegrationDomain;
using Moq;
using Xunit;

namespace Infrastructure.Tests;

public class IntegrationServiceTests
{
    [Fact]
    public async Task GetMovieInformationByMovieIdAsync_ValidId_CallsCollectMovieInformation()
    {
        // Arrange
        var movieId = Guid.NewGuid();
        var mockRepository = new Mock<IIntegrationRepository>();
        var mockRequestHandler = new Mock<IRequestHandler>();

        var mockHandlers = new List<IRequestHandler>();

        var expectedIntegration = new Integration();

        mockRepository
            .Setup(r => r.GetByMovieIdAsync(movieId))
            .ReturnsAsync(expectedIntegration);

        mockRequestHandler
            .Setup(r => r.CollectMovieInformation(It.IsAny<Integration>(), It.IsAny<MovieInformationDto>()))
            .Returns(Task.CompletedTask);

        var service = new IntegrationService(mockRepository.Object, mockHandlers);

        // Act
        var result = await service.GetMovieInformationByMovieIdAsync(movieId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("", result.Director);
        Assert.Empty(result.ActorNames);
        Assert.Equal("", result.Awards);
        Assert.NotNull(result.Rating);
        Assert.Equal(0.0, result.Rating.AggregatedValue);
        Assert.Empty(result.Rating.Ratings);

        mockRepository
            .Verify(r => r.GetByMovieIdAsync(movieId), Times.Once);
        mockRequestHandler
            .Verify(r => r.CollectMovieInformation(expectedIntegration, result), Times.Once);
    }
}