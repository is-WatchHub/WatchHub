using Moq;
using UserManagementApplication.Dtos;
using UserManagementApplication.Mappers;
using UserManagementApplication.Services;
using UserManagementDomain;
using Xunit;

namespace Infrastructure.Tests;

public class UserManagementServiceTests
{
    private readonly Mock<IUserManagementMapper> _mapperMock;
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly UserManagementService _userManagementService;

    public UserManagementServiceTests()
    {
        _mapperMock = new Mock<IUserManagementMapper>();
        _repositoryMock = new Mock<IUserRepository>();
        
        _userManagementService = new UserManagementService(_mapperMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public async Task GetByUserNameAsync_ShouldReturnUserDto_WhenUserExists()
    {
        // Arrange
        var username = "user";
        var email = "user@example.com";
        var user = new User { UserName = username, Email = email };
        var userDto = new UserDto { UserName = username, Email = email };
        
        _repositoryMock
            .Setup(repo => repo.GetByUserNameAsync(username))
            .ReturnsAsync(user);
        
        _mapperMock
            .Setup(mapper => mapper.Map<User, UserDto>(user))
            .Returns(userDto);
        
        // Act
        var result = await _userManagementService.GetByUserNameAsync(username);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.UserName, result.UserName);
        Assert.Equal(userDto.Email, result.Email);
        
        _repositoryMock
            .Verify(repo => repo.GetByUserNameAsync(username), Times.Once);
        _mapperMock
            .Verify(mapper => mapper.Map<User, UserDto>(user), Times.Once);
    }
}