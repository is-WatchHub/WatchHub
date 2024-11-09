using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using UserManagementApplication;
using UserManagementApplication.Dtos;
using UserManagementApplication.Dtos.Incoming;
using UserManagementApplication.Dtos.Outgoing;
using UserManagementApplication.Services;

namespace Infrastructure.Services;

public class UserManagementService : IUserManagementService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthenticationService _authService;

    public UserManagementService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthenticationService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    public async Task<SignInResultDto> Login(LoginDto model) =>
        await _authService.Login(model);



    public async Task Logout() =>
        await _authService.Logout();


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

    public Task<GetUserResultDto?> GetUser(Guid id)
    {
        var foundUser = _userManager.Users.FirstOrDefault(u => u.Id.Equals(id.ToString()));
        if(foundUser == null) return Task.FromResult<GetUserResultDto?>(null);
        
        return Task.FromResult(new GetUserResultDto(foundUser.Id)
        {
            Email = foundUser.Email,
            Login = foundUser.UserName
        });
    }

    public Task<GetUserResultDto?> GetUser(string username)
    {
        var foundUser = _userManager.Users.FirstOrDefault(u => u.UserName != null && u.UserName.Equals(username));
        if(foundUser == null) return Task.FromResult<GetUserResultDto?>(null);
        
        return Task.FromResult(new GetUserResultDto(foundUser.Id)
        {
            Email = foundUser.Email,
            Login = foundUser.UserName
        });
    }
}