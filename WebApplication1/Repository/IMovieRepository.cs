using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie GetById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
        void SaveChanges();
    }
}
