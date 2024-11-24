using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService) => 
        _moviesService = moviesService ?? throw new ArgumentNullException(nameof(moviesService));

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _moviesService.GetAsync();
        
        return Ok(movies);
    }

    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMovieById(Guid id)
    {
        var movie = await _moviesService.GetByIdAsync(id);
        
        return Ok(movie);
    }

    [HttpGet("{id:guid}/info")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetAdditionalInfoById(Guid id)
    {
        var info = await _moviesService.GetInfoByIdAsync(id);
        
        return Ok(info);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> AddMovie([FromBody] CreateMovieDto createMovieDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var addedMovie = await _moviesService.AddAsync(createMovieDto);
        
        return CreatedAtAction(nameof(GetMovieById), new { id = addedMovie.Id }, addedMovie);
    }

    [HttpGet("filter")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMoviesByFilter([FromQuery] MovieFilterDto filterDto)
    {
        var movies = await _moviesService.GetByFilterAsync(filterDto);
        
        return Ok(movies);
    }
}
