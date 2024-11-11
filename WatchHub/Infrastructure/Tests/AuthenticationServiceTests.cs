using Infrastructure.Dtos;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Tests;

public class AuthenticationServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly AuthenticationService _authenticationService;

    public AuthenticationServiceTests()
    {
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            null, null, null, null, null, null, null, null);

        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            httpContextAccessorMock.Object, 
            claimsPrincipalFactoryMock.Object,
            null, null, null);

        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        _signInManagerMock
            .Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);

        _signInManagerMock
            .Setup(sm => sm.SignOutAsync())
            .Returns(Task.CompletedTask);

        _authenticationService = new AuthenticationService(_signInManagerMock.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnSuccess_WhenUserIsCreated()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            UserName = "user",
            Email = "user@example.com",
            Password = "Test123!"
        };

        // Act
        var result = await _authenticationService.CreateAsync(createUserDto);

        // Assert
        Assert.True(result.Succeeded);
        
        _userManagerMock
            .Verify(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnSuccess_WhenCredentialsAreCorrect()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            UserName = "user",
            Password = "Test123!",
            RememberMe = true
        };

        // Act
        var result = await _authenticationService.LoginAsync(loginDto);

        // Assert
        Assert.Equal(SignInResult.Success, result);
        
        _signInManagerMock
            .Verify(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
    }

    [Fact]
    public async Task LogoutAsync_ShouldNotThrow_WhenLoggedOut()
    {
        // Act
        await _authenticationService.LogoutAsync();

        // Assert
        _signInManagerMock.Verify(sm => sm.SignOutAsync(), Times.Once);
    }
}
