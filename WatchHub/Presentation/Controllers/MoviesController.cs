using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outgoing;
using MoviesApplication.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = new List<MovieInfoResponseDto>();
        return Ok(movies);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMovieById(Guid id)
    {
        var movie = await _moviesService.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound(new { Message = "Movie not found" });
        }
        return Ok(movie);
    }

    [HttpGet("{id:guid}/info")]
    public async Task<IActionResult> GetAdditionalInfoById(Guid id)
    {
        var info = await _moviesService.GetInfoByIdAsync(id);
        if (info == null)
        {
            return NotFound(new { Message = "Movie info not found" });
        }
        return Ok(info);
    }

    [HttpPost]
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
    public async Task<IActionResult> GetMoviesByFilter([FromQuery] MovieFilterDto filterDto)
    {
        var movies = await _moviesService.GetByFilterAsync(filterDto);
        return Ok(movies);
    }
}
