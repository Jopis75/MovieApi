using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.Movies;
using MovieApi.Models;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(MovieDbContext context, IMapper mapper, IValidator<CreateMovieWithMovieDetailsDto> createMovieWithMovieDetailsDtoValidator, IValidator<CreateMovieWithMovieDetailsAndGenresDto> createMovieWithMovieDetailsAndGenresDtoValidator, IValidator<UpdateMovieWithMovieDetailsDto> updateMovieWithMoviedDetailsDtoValidator) : ControllerBase
{
    [HttpPost]
    [Route("CreateWithMovieDetailsAndGenresAsync")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> CreateWithMovieDetailsAndGenresAsync(CreateMovieWithMovieDetailsAndGenresDto createMovieWithMovieDetailsAndGenresDto)
    {
        try
        {
            if (createMovieWithMovieDetailsAndGenresDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await createMovieWithMovieDetailsAndGenresDtoValidator.ValidateAsync(createMovieWithMovieDetailsAndGenresDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var movieDetails = mapper.Map<MovieDetails>(createMovieWithMovieDetailsAndGenresDto);

            await context.MovieDetails.AddAsync(movieDetails);

            await context.SaveChangesAsync();

            var movie = mapper.Map<Movie>(createMovieWithMovieDetailsAndGenresDto);
            movie.MovieDetailsId = movieDetails.Id;

            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();

            var movieGenres = createMovieWithMovieDetailsAndGenresDto.GenreIds.Select(genreId => new MovieGenre { MovieId = movie.Id, GenreId = genreId });

            await context.MovieGenres.AddRangeAsync(movieGenres);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, mapper.Map<MovieDto>(movie));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("CreateWithMovieDetailsAsync")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> CreateWithMovieDetailsAsync(CreateMovieWithMovieDetailsDto createMovieWithMovieDetailsDto)
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

            await context.MovieDetails.AddAsync(movieDetails);
            await context.SaveChangesAsync();

            var movie = mapper.Map<Movie>(createMovieWithMovieDetailsDto);
            movie.MovieDetailsId = movieDetails.Id;

            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, mapper.Map<MovieDto>(movie));
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
            if (id == null || id <= 0)
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
    [Route("UpdateWithMovieDetailsAsync")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateWithMovieDetailsAsync(UpdateMovieWithMovieDetailsDto updateMovieWithMovieDetailsDto)
    {
        try
        {
            if (updateMovieWithMovieDetailsDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await updateMovieWithMoviedDetailsDtoValidator.ValidateAsync(updateMovieWithMovieDetailsDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var movie = mapper.Map<Movie>(updateMovieWithMovieDetailsDto);

            context.Movies.Update(movie);
            await context.SaveChangesAsync();

            var movieDetails = mapper.Map<MovieDetails>(updateMovieWithMovieDetailsDto);

            context.MovieDetails.Update(movieDetails);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Movies.Any(movie => movie.Id == updateMovieWithMovieDetailsDto.Id))
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
