using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(
        MovieService _movieService,
        IValidator<CreateMovieRequest> _createValidator,
        IValidator<UpdateMovieRequest> _updateValidator,
        IMapper _mapper) : ControllerBase
    {
        [HttpGet("movies")]
        public ActionResult<List<MovieResponse>> GetAllMovies()
        {
            var response = _mapper.Map<List<MovieResponse>>(_movieService.GetAll());
            return Ok(response);
        }

        [HttpGet("movies/{id:int}")]
        public ActionResult<MovieResponse> GetMovie(int id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
                return NotFound($"Movie with id {id} not found.");

            return Ok(_mapper.Map<MovieResponse>(movie));
        }

        [HttpPost("movies")]
        public ActionResult<MovieResponse> AddMovie([FromBody] CreateMovieRequest request)
        {
            var validationResult = _createValidator.Validate(request);
            if (!validationResult.IsValid)
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            var movie = _mapper.Map<Movie>(request);
            _movieService.Add(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, _mapper.Map<MovieResponse>(movie));
        }

        [HttpPut("movies/{id:int}")]
        public ActionResult<MovieResponse> UpdateMovie(int id, [FromBody] UpdateMovieRequest request)
        {
            var validationResult = _updateValidator.Validate(request);
            if (!validationResult.IsValid)
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            var movie = _movieService.GetById(id);
            if (movie == null)
                return NotFound($"Movie with id {id} not found.");

            _mapper.Map(request, movie);
            _movieService.Update(movie);
            return Ok(_mapper.Map<MovieResponse>(movie));
        }

        [HttpDelete("movies/{id:int}")]
        public ActionResult DeleteMovie(int id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
                return NotFound($"Movie with id {id} not found.");

            _movieService.Delete(movie);
            return NoContent();
        }

    }
}
