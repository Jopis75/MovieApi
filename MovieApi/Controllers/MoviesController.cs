using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.Movies;
using MovieApi.Models;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(MovieDbContext context, IMapper mapper, IValidator<CreateMovieDto> createMovieDtoValidator, IValidator<UpdateMovieDto> updateMovieDtoValidator) : ControllerBase
{
    private readonly MovieDbContext _context = context;

    [HttpPost]
    [Route("CreateAsync")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Movie>> CreateAsync(CreateMovieDto createMovieDto)
    {
        try
        {
            if (createMovieDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await createMovieDtoValidator.ValidateAsync(createMovieDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var movie = mapper.Map<Movie>(createMovieDto);

            var entityEntry = await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, mapper.Map<MovieDto>(entityEntry.Entity));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteAsync/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        try
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ReadAllAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> ReadAllAsync()
    {
        try
        {
            var movies = await _context
                .Movies
                //.Include(movie => movie.Genre)
                .Include(movie => movie.MovieDetails)
                .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, movies.Select(mapper.Map<MovieDto>));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ReadByIdAsync")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> ReadByIdAsync(int id)
    {
        try
        {
            var movie = await _context
                .Movies
                //.Include(movie => movie.Genre)
                .Include(movie => movie.MovieDetails)
                .Where(movie => movie.Id == id)
                .SingleOrDefaultAsync();

            if (movie == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, mapper.Map<MovieDto>(movie));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [Route("UpdateAsync")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(UpdateMovieDto updateMovieDto)
    {
        try
        {
            if (updateMovieDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await updateMovieDtoValidator.ValidateAsync(updateMovieDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var movie = mapper.Map<Movie>(updateMovieDto);

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Movies.Any(e => e.Id == updateMovieDto.Id))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
