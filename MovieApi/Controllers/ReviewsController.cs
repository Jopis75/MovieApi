using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.Reviews;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController(
        MovieDbContext context,
        IMapper mapper,
        IValidator<CreateReviewDto> createReviewDtoValidator,
        IValidator<UpdateReviewDto> updateReviewDtoValidator) : ControllerBase
    {
        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewDto>> CreateAsync(CreateReviewDto createReviewDto)
        {
            try
            {
                if (createReviewDtoValidator == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                var validationResult = await createReviewDtoValidator.ValidateAsync(createReviewDto);

                if (!validationResult.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
                }

                var review = mapper.Map<Review>(createReviewDto);

                await context.Reviews.AddAsync(review);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, mapper.Map<ReviewDto>(review));
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

                var review = await context.Reviews.FindAsync(id);

                if (review == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                context.Reviews.Remove(review);
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
        [ProducesResponseType(typeof(IEnumerable<ReviewWithMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewWithMovieDto>>> ReadAllAsync()
        {
            try
            {
                var reviews = await context
                    .Reviews
                    .Include(review => review.Movie)
                    .ToListAsync();

                return StatusCode(StatusCodes.Status200OK, reviews.Select(mapper.Map<ReviewWithMovieDto>));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("ReadByIdAsync")]
        [ProducesResponseType(typeof(ReviewWithMovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewWithMovieDto>> ReadByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "id must be greater than 0.");
                }

                var review = await context
                    .Reviews
                    .Include(review => review.Movie)
                    .Where(actor => actor.Id == id)
                    .SingleOrDefaultAsync();

                if (review == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status200OK, mapper.Map<ReviewWithMovieDto>(review));
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
        public async Task<IActionResult> UpdateAsync(UpdateReviewDto updateReviewDto)
        {
            try
            {
                if (updateReviewDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                var validationResult = await updateReviewDtoValidator.ValidateAsync(updateReviewDto);

                if (!validationResult.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
                }

                var review = mapper.Map<Review>(updateReviewDto);

                context.Reviews.Update(review);
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
