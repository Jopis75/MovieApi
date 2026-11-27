using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.Genres;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(
        MovieDbContext context,
        IMapper mapper,
        IValidator<CreateGenreDto> createGenreDtoValidator,
        IValidator<CreateGenreWithMovieDto> createGenreWithMovieDtoValidator,
        IValidator<UpdateGenreDto> updateGenreDtoValidator) : ControllerBase
    {
        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreDto>> CreateAsync(CreateGenreDto createGenreDto)
        {
            try
            {
                if (createGenreDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                var validationResult = await createGenreDtoValidator.ValidateAsync(createGenreDto);

                if (!validationResult.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
                }

                var genre = mapper.Map<Genre>(createGenreDto);

                await context.Genres.AddAsync(genre);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, mapper.Map<GenreDto>(genre));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateWithMovieAsync")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreDto>> CreateWithMovieAsync(CreateGenreWithMovieDto createGenreWithMovieDto)
        {
            try
            {
                if (createGenreWithMovieDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                var validationResult = await createGenreWithMovieDtoValidator.ValidateAsync(createGenreWithMovieDto);

                if (!validationResult.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
                }

                var genre = mapper.Map<Genre>(createGenreWithMovieDto);

                await context.Genres.AddAsync(genre);
                await context.SaveChangesAsync();

                var movieGenre = mapper.Map<MovieGenre>(createGenreWithMovieDto);
                movieGenre.GenreId = genre.Id;

                await context.MovieGenres.AddAsync(movieGenre);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, mapper.Map<GenreDto>(genre));
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

                var genre = await context.Genres.FindAsync(id);

                if (genre == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                var movieGenres = await context
                    .MovieGenres
                    .Where(movieGenre => movieGenre.GenreId == genre.Id)
                    .ToListAsync();

                context.MovieGenres.RemoveRange(movieGenres);
                await context.SaveChangesAsync();

                context.Genres.Remove(genre);
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
        [ProducesResponseType(typeof(IEnumerable<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GenreDto>>> ReadAllAsync()
        {
            try
            {
                var genres = await context
                    .Genres
                    .ToListAsync();

                return StatusCode(StatusCodes.Status200OK, genres.Select(mapper.Map<GenreDto>));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("ReadByIdAsync")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreDto>> ReadByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "id must be greater than 0.");
                }

                var genre = await context
                    .Genres
                    .Where(genre => genre.Id == id)
                    .SingleOrDefaultAsync();

                if (genre == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status200OK, mapper.Map<GenreDto>(genre));
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateGenreDto updateGenreDto)
        {
            try
            {
                if (updateGenreDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                var validationResult = await updateGenreDtoValidator.ValidateAsync(updateGenreDto);

                if (!validationResult.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
                }

                var exists = await context
                    .Genres
                    .AnyAsync(genre => genre.Id == updateGenreDto.Id);

                if (!exists)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                var genre = mapper.Map<Genre>(updateGenreDto);

                context.Genres.Update(genre);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
