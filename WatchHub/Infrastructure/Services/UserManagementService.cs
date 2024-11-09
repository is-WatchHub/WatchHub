using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using UserManagementApplication;
using UserManagementApplication.Dtos;
using UserManagementApplication.Dtos.Results;

namespace Infrastructure.Services;

public class UserManagementService : IUserManagementService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserManagementService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResultDto> Login(LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
        return new SignInResultDto(result.Succeeded, result.IsLockedOut, result.IsNotAllowed);
    }


    public async Task Logout() =>
        await _signInManager.SignOutAsync();


    public async Task<CreateUserResultDto> CreateUser(CreateUserDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Name,
            Email = dto.Email
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        return new CreateUserResultDto(result.Succeeded, result.Errors.Select(x => $"{x.Code} -- {x.Description}"));
    }
}