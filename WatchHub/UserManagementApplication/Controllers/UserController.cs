using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementApplication.Dtos;

namespace UserManagementApplication.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);

        if (result.Succeeded)
        {
            return Ok(new { Message = "Login successful" });
        }

        return Unauthorized(new { Message = "Invalid login attempt" });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "Logout successful" });
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Name,
            Email = dto.Email
        };
        
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User created successfully" });
        }

        return BadRequest(result.Errors);
    }
}