using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
        Task SaveChangesAsync();
    }
}
