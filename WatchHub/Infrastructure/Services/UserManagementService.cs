using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using UserManagementApplication.Dtos;
using UserManagementApplication.Interfaces;

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
    
    public async Task Login(LoginDto model) => 
        await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
    
    public async Task Logout() =>
        await _signInManager.SignOutAsync();
    
    
    public async Task CreateUser(CreateUserDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Name,
            Email = dto.Email
        };
        await _userManager.CreateAsync(user, dto.Password);
    }
}