using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.Movies;
using MovieApi.Models;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(MovieDbContext context, IMapper mapper, IValidator<CreateMovieWithMovieDetailsDto> createMovieWithMovieDetailsDtoValidator, IValidator<UpdateMovieDto> updateMovieDtoValidator) : ControllerBase
{
    [HttpPost]
    [Route("CreateAsync")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Movie>> CreateAsync(CreateMovieWithMovieDetailsDto createMovieWithMovieDetailsDto)
    {
        try
        {
            if (createMovieWithMovieDetailsDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await createMovieWithMovieDetailsDtoValidator.ValidateAsync(createMovieWithMovieDetailsDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var movieDetails = mapper.Map<MovieDetails>(createMovieWithMovieDetailsDto);

            var entityEntryMovieDetails = await context.MovieDetails.AddAsync(movieDetails);
            await context.SaveChangesAsync();

            var movie = mapper.Map<Movie>(createMovieWithMovieDetailsDto);
            movie.MovieDetailsId = movieDetails.Id;

            var entityEntryMovie = await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, mapper.Map<MovieDto>(entityEntryMovie.Entity));
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

            var movie = await context.Movies.FindAsync(id);

            if (movie == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            context.Movies.Remove(movie);
            await context.SaveChangesAsync();

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
            var movies = await context
                .Movies
                .Include(movie => movie.MovieDetails)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(movieGenre => movieGenre.Genre)
                .Include(movie => movie.MovieActors)
                    .ThenInclude(movieActor => movieActor.Actor)
                .Include(movie => movie.Reviews)
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
            var movie = await context
                .Movies
                .Include(movie => movie.MovieDetails)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(movieGenre => movieGenre.Genre)
                .Include(movie => movie.MovieActors)
                    .ThenInclude(movieActor => movieActor.Actor)
                .Include(movie => movie.Reviews)
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

            context.Movies.Update(movie);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Movies.Any(e => e.Id == updateMovieDto.Id))
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
