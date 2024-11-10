using Infrastructure.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public interface IAuthenticationService
{
    Task<IdentityResult> CreateAsync(CreateUserDto createUserDto);
    Task<SignInResult> LoginAsync(LoginDto loginDto);
    Task LogoutAsync();
}