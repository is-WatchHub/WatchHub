using Microsoft.AspNetCore.Mvc;
using UserManagementApplication.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/")]
public class UserManagementController : ControllerBase
{
    private readonly IUserManagementService _userManagementService;

    public UserManagementController(IUserManagementService userManagementService) =>
        _userManagementService =
            userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
    
    [HttpGet("/users")]
    public async Task<IActionResult> Register([FromQuery] string name)
    {
        var result = await _userManagementService.GetByUserNameAsync(name);

        return Ok(result);
    }
}