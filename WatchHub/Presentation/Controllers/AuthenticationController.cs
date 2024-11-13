using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/")]
public class AuthenticationController
{
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService) =>
        _authenticationService =
            authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

    [HttpPost("/users")]
    public async Task<ActionResult<IdentityResult>> Register(CreateUserDto createUserDto) =>
        await _authenticationService.CreateUserAsync(createUserDto);
}