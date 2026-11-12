using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IMovieCommentRepository
    {
        List<MovieComment> GetByMovieId(int movieId);
        MovieComment GetById(int id);
        void Add(MovieComment comment);
        void Update(MovieComment comment);
        void Delete(MovieComment comment);
        void SaveChanges();
    }
}
