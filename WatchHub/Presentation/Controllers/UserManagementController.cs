using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApplication.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserManagementController : ControllerBase
{
    private readonly IUserManagementService _userManagementService;

    public UserManagementController(IUserManagementService userManagementService) =>
        _userManagementService =
            userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
    
    [HttpGet]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetByUserNameAsync([FromQuery] string name)
    {
        var result = await _userManagementService.GetByUserNameAsync(name);

        return Ok(result);
    }
}