using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/Movie")]
    [ApiController]
    public class MovieCommentController(
        MovieCommentService _commentService,
        IValidator<CreateMovieCommentRequest> _createValidator,
        IValidator<UpdateMovieCommentRequest> _updateValidator,
        IMapper _mapper) : ControllerBase
    {
        [HttpGet("movies/{movieId:int}/comments")]
        public ActionResult<List<MovieCommentResponse>> GetComments(int movieId)
        {
            if (!_commentService.MovieExists(movieId))
                return NotFound($"Movie with id {movieId} not found.");

            var response = _mapper.Map<List<MovieCommentResponse>>(_commentService.GetByMovieId(movieId));
            return Ok(response);
        }

        [HttpPost("movies/{movieId:int}/comments")]
        public ActionResult<MovieCommentResponse> AddComment(int movieId, [FromBody] CreateMovieCommentRequest request)
        {
            var validationResult = _createValidator.Validate(request);
            if (!validationResult.IsValid)
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            if (!_commentService.MovieExists(movieId))
                return NotFound($"Movie with id {movieId} not found.");

            var comment = _mapper.Map<MovieComment>(request);
            comment.MovieId = movieId;
            _commentService.Add(comment);
            return CreatedAtAction(nameof(GetComments), new { movieId }, _mapper.Map<MovieCommentResponse>(comment));
        }

        [HttpPut("movies/{movieId:int}/comments/{id:int}")]
        public ActionResult<MovieCommentResponse> UpdateComment(int movieId, int id, [FromBody] UpdateMovieCommentRequest request)
        {
            var validationResult = _updateValidator.Validate(request);
            if (!validationResult.IsValid)
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            var comment = FindComment(movieId, id);
            if (comment == null)
                return NotFound($"Comment with id {id} not found for movie {movieId}.");

            _mapper.Map(request, comment);
            _commentService.Update(comment);
            return Ok(_mapper.Map<MovieCommentResponse>(comment));
        }

        [HttpDelete("movies/{movieId:int}/comments/{id:int}")]
        public ActionResult DeleteComment(int movieId, int id)
        {
            var comment = FindComment(movieId, id);
            if (comment == null)
                return NotFound($"Comment with id {id} not found for movie {movieId}.");

            _commentService.Delete(comment);
            return NoContent();
        }

        private MovieComment FindComment(int movieId, int id)
        {
            var comment = _commentService.GetById(id);
            return comment != null && comment.MovieId == movieId ? comment : null;
        }
    }
}
