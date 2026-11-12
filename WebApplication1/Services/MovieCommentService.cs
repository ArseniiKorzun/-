using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class MovieCommentService(IMovieCommentRepository _commentRepository, MovieService _movieService)
    {
        public bool MovieExists(int movieId) => _movieService.GetById(movieId) != null;

        public List<MovieComment> GetByMovieId(int movieId) => _commentRepository.GetByMovieId(movieId);

        public MovieComment GetById(int id) => _commentRepository.GetById(id);

        public void Add(MovieComment comment)
        {
            _commentRepository.Add(comment);
            _commentRepository.SaveChanges();
        }

        public void Update(MovieComment comment)
        {
            _commentRepository.Update(comment);
            _commentRepository.SaveChanges();
        }

        public void Delete(MovieComment comment)
        {
            _commentRepository.Delete(comment);
            _commentRepository.SaveChanges();
        }
    }
}
