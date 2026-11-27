using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.MovieDetails;
using MovieApi.Dtos.Movies;
using MovieApi.Models;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(
    MovieDbContext context,
    IMapper mapper,
    IValidator<CreateMovieWithMovieDetailsDto> createMovieWithMovieDetailsDtoValidator,
    IValidator<CreateMovieWithMovieDetailsAndGenresDto> createMovieWithMovieDetailsAndGenresDtoValidator,
    IValidator<UpdateMovieWithMovieDetailsDto> updateMovieWithMovieDetailsDtoValidator,
    IValidator<UpdateMovieDetailsDto> updateMovieDetailsDtoValidator,
    IValidator<UpdateMovieGenresDto> updateMovieGenresDtoValidator) : ControllerBase
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

            var movieGenres = createMovieWithMovieDetailsAndGenresDto
                .GenreIds
                .Select(genreId => new MovieGenre { MovieId = movie.Id, GenreId = genreId })
                .ToList();

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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "id must be greater than 0.");
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
    [Route("ReadByActorNameAndGenreNameAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> ReadByActorNameAndGenreNameAsync([FromQuery] string actorName, [FromQuery] string genreName)
    {
        try
        {
            if (string.IsNullOrEmpty(actorName))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "actorName is required.");
            }

            if (string.IsNullOrEmpty(genreName))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "genreName is required.");
            }

            var movies = await context
                .Movies
                .Include(movie => movie.MovieDetails)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(movieGenre => movieGenre.Genre)
                .Include(movie => movie.MovieActors)
                    .ThenInclude(movieActor => movieActor.Actor)
                .Include(movie => movie.Reviews)
                .Where(movie => movie
                    .MovieActors
                    .Any(movieActor => movieActor.Actor != null
                        && movieActor.Actor.Name != null
                        && movieActor.Actor.Name == actorName)
                    && movie
                    .MovieGenres
                    .Any(movieGenre => movieGenre.Genre != null
                        && movieGenre.Genre.Name != null
                        && movieGenre.Genre.Name == genreName))
                .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, movies.Select(mapper.Map<MovieDto>));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ReadByActorNameAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> ReadByActorNameAsync([FromQuery] string actorName)
    {
        try
        {
            if (string.IsNullOrEmpty(actorName))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "actorName is required.");
            }

            var movies = await context
                .Movies
                .Include(movie => movie.MovieDetails)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(movieGenre => movieGenre.Genre)
                .Include(movie => movie.MovieActors)
                    .ThenInclude(movieActor => movieActor.Actor)
                .Include(movie => movie.Reviews)
                .Where(movie => movie
                    .MovieActors
                    .Any(movieActor => movieActor.Actor != null
                        && movieActor.Actor.Name != null
                        && movieActor.Actor.Name == actorName))
                .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, movies.Select(mapper.Map<MovieDto>));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ReadByGenreNameAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> ReadByGenreNameAsync([FromQuery] string genreName)
    {
        try
        {
            if (string.IsNullOrEmpty(genreName))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "genreName is required.");
            }

            var movies = await context
                .Movies
                .Include(movie => movie.MovieDetails)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(movieGenre => movieGenre.Genre)
                .Include(movie => movie.MovieActors)
                    .ThenInclude(movieActor => movieActor.Actor)
                .Include(movie => movie.Reviews)
                .Where(movie => movie
                    .MovieGenres
                    .Any(movieGenre => movieGenre.Genre != null
                        && movieGenre.Genre.Name != null
                        && movieGenre.Genre.Name == genreName))
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
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "id must be greater than 0.");
            }

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

    [HttpGet]
    [Route("ReadByReviewerNameAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> ReadByReviewerNameAsync(string reviewerName)
    {
        try
        {
            if (string.IsNullOrEmpty(reviewerName))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "reviewerName is required.");
            }

            var movies = await context
                .Movies
                .Include(movie => movie.MovieDetails)
                .Include(movie => movie.MovieGenres)
                    .ThenInclude(movieGenre => movieGenre.Genre)
                .Include(movie => movie.MovieActors)
                    .ThenInclude(movieActor => movieActor.Actor)
                .Include(movie => movie.Reviews)
                .Where(movie => movie
                    .Reviews
                    .Any(review => review.ReviewerName == reviewerName))
                .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, movies.Select(mapper.Map<MovieDto>));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [Route("UpdateGenresAsync")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateGenresAsync(UpdateMovieGenresDto updateMovieGenresDto)
    {
        try
        {
            if (updateMovieGenresDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await updateMovieGenresDtoValidator.ValidateAsync(updateMovieGenresDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var oldMovieGenres = await context
                .MovieGenres
                .Where(movieGenre => movieGenre.MovieId == updateMovieGenresDto.MovieId)
                .ToListAsync();

            context.MovieGenres.RemoveRange(oldMovieGenres);
            await context.SaveChangesAsync();

            var newMovieGenres = updateMovieGenresDto
                .GenreIds
                .Select(genreId => new MovieGenre { MovieId = updateMovieGenresDto.MovieId, GenreId = genreId })
                .ToList();

            await context.MovieGenres.AddRangeAsync(newMovieGenres);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [Route("UpdateMovieDetailsAsync")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateMovieDetailsAsync(UpdateMovieDetailsDto updateMovieDetailsDto)
    {
        try
        {
            if (updateMovieDetailsDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await updateMovieDetailsDtoValidator.ValidateAsync(updateMovieDetailsDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var movieDetails = mapper.Map<MovieDetails>(updateMovieDetailsDto);

            context.MovieDetails.Update(movieDetails);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
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

            var validationResult = await updateMovieWithMovieDetailsDtoValidator.ValidateAsync(updateMovieWithMovieDetailsDto);

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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
