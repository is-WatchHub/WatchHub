using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using UserManagementApplication.Dtos.Incoming;
using UserManagementApplication.Dtos.Outgoing;
using UserManagementApplication.Services;

namespace Infrastructure.Services;

public class AuthenticationService(SignInManager<ApplicationUser> _signInManager) : IAuthenticationService
{
    public async Task<SignInResultDto> Login(LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
        return new SignInResultDto(result.Succeeded, result.IsLockedOut, result.IsNotAllowed);
    }

    public async Task Logout() =>
        await _signInManager.SignOutAsync();
    
}