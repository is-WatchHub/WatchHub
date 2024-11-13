using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService) =>
        _authenticationService =
            authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

    [HttpPost("/users")]
    public async Task<IActionResult> Register(CreateUserDto createUserDto)
    {
        var result = await _authenticationService.CreateUserAsync(createUserDto);

        if (result.Succeeded) return Ok(result);

        return BadRequest();
    }
}