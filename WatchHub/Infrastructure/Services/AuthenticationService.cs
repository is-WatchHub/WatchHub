using Infrastructure.Dtos;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    public AuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new ApplicationUser
        {
            UserName = createUserDto.UserName,
            Email = createUserDto.Email
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        return result;
    }

    public async Task<SignInResult> LoginAsync(LoginDto loginDto) =>
        await _signInManager
            .PasswordSignInAsync(loginDto.UserName, loginDto.Password, loginDto.RememberMe, false);

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();
}