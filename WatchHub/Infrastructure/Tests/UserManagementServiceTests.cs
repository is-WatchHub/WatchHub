using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using UserManagementApplication.Dtos;
using Xunit;

namespace Infrastructure.Tests;

public class UserManagementServiceTests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private UserManagementService _userManagementService;

    [Fact]
    public async Task Login_Should_Call_PasswordSignInAsync_With_Correct_Parameters()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Name = "testUser",
            Password = "password123",
            RememberMe = true
        };

        Arrange(new List<ApplicationUser>());

        // Act
        await _userManagementService.Login(loginDto);

        // Assert
        _signInManagerMock.Verify(
            s => s.PasswordSignInAsync(loginDto.Name, loginDto.Password, loginDto.RememberMe, false), Times.Once);
    }

    [Fact]
    public async Task Logout_Should_Call_SignOutAsync()
    {
        // Arrange
        Arrange(new List<ApplicationUser>());
        // Act
        await _userManagementService.Logout();

        // Assert
        _signInManagerMock.Verify(s => s.SignOutAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateUser_Should_Call_CreateAsync_With_Correct_Parameters()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            Name = "testUser",
            Email = "test@example.com",
            Password = "password123"
        };

        var user = new ApplicationUser
        {
            UserName = createUserDto.Name,
            Email = createUserDto.Email
        };
        
        Arrange(new List<ApplicationUser>(){user});

        _userManagerMock.Setup(u =>
                u.CreateAsync(It.Is<ApplicationUser>(x => x.UserName == user.UserName && x.Email == user.Email),
                    createUserDto.Password))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        await _userManagementService.CreateUser(createUserDto);

        // Assert
        _userManagerMock.Verify(
            u => u.CreateAsync(It.Is<ApplicationUser>(x => x.UserName == user.UserName && x.Email == user.Email),
                createUserDto.Password), Times.Once);
    }

    private void Arrange(List<ApplicationUser> users)
    {
        _userManagerMock = MockUserManager<ApplicationUser>(users);
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<ILogger<SignInManager<ApplicationUser>>>(),
            Mock.Of<IAuthenticationSchemeProvider>());
        _userManagementService = new UserManagementService(_userManagerMock.Object, _signInManagerMock.Object);
    }

    private static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success)
            .Callback<TUser, string>((x, y) => ls.Add(x));
        mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

        return mgr;
    }
}