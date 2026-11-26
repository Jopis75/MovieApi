using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.Actors;
using MovieApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ActorsController(
    MovieDbContext context,
    IMapper mapper,
    IValidator<CreateActorDto> createActorDtoValidator,
    IValidator<CreateActorWithMovieDto> createActorWithMovieDtoValidator,
    IValidator<UpdateActorDto> updateActorDtoValidator) : ControllerBase
{
    [HttpPost]
    [Route("CreateAsync")]
    [ProducesResponseType(typeof(ActorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ActorDto>> CreateAsync(CreateActorDto createActorDto)
    {
        try
        {
            if (createActorDtoValidator == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await createActorDtoValidator.ValidateAsync(createActorDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var actor = mapper.Map<Actor>(createActorDto);

            await context.Actors.AddAsync(actor);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, mapper.Map<ActorDto>(actor));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("CreateWithMovieAsync")]
    [ProducesResponseType(typeof(ActorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ActorDto>> CreateWithMovieAsync(CreateActorWithMovieDto createActorWithMovieDto)
    {
        try
        {
            if (createActorWithMovieDtoValidator == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await createActorWithMovieDtoValidator.ValidateAsync(createActorWithMovieDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var actor = mapper.Map<Actor>(createActorWithMovieDto);

            await context.Actors.AddAsync(actor);
            await context.SaveChangesAsync();

            var movieActor = mapper.Map<MovieActor>(createActorWithMovieDto);
            movieActor.ActorId = actor.Id;

            await context.MovieActors.AddAsync(movieActor);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, mapper.Map<ActorDto>(actor));
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

            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "id must be greater than 0.");
            }

            var actor = await context.Actors.FindAsync(id);

            if (actor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var movieActors = await context
                .MovieActors
                .Where(movieActor => movieActor.ActorId == id)
                .ToListAsync();

            context.MovieActors.RemoveRange(movieActors);
            await context.SaveChangesAsync();

            context.Actors.Remove(actor);
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
    [ProducesResponseType(typeof(IEnumerable<ActorWithMoviesDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ActorWithMoviesDto>>> ReadAllAsync()
    {
        try
        {
            var actors = await context
                .Actors
                .Include(actor => actor.MovieActors)
                    .ThenInclude(movieActor => movieActor.Movie)
                .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, actors.Select(mapper.Map<ActorWithMoviesDto>));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("ReadByIdAsync")]
    [ProducesResponseType(typeof(ActorWithMoviesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ActorWithMoviesDto>> ReadByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "id must be greater than 0.");
            }

            var actor = await context
                .Actors
                .Include(actor => actor.MovieActors)
                    .ThenInclude(movieActor => movieActor.Movie)
                .Where(actor => actor.Id == id)
                .SingleOrDefaultAsync();

            if (actor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, mapper.Map<ActorWithMoviesDto>(actor));
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
    public async Task<IActionResult> UpdateAsync(UpdateActorDto updateActorDto)
    {
        try
        {
            if (updateActorDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var validationResult = await updateActorDtoValidator.ValidateAsync(updateActorDto);

            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            var actor = mapper.Map<Actor>(updateActorDto);

            context.Actors.Update(actor);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
