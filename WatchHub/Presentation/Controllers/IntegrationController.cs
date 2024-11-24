using IntegrationApplication.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/integrations")]
public class IntegrationController : ControllerBase
{
    private readonly IIntegrationService _integrationService;

    public IntegrationController(IIntegrationService integrationService) =>
        _integrationService = integrationService ?? throw new ArgumentNullException(nameof(integrationService));
    
    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMovieById(Guid id)
    {
        var movieInformation = await _integrationService.GetMovieInformationByMovieIdAsync(id);
        
        return Ok(movieInformation);
    }
}