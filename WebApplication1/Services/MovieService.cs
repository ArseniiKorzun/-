using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class MovieService(IMovieRepository _movieRepository)
    {
        public List<Movie> GetAll() => _movieRepository.GetAll();

        public Movie GetById(int id) => _movieRepository.GetById(id);

        public void Add(Movie movie)
        {
            _movieRepository.Add(movie);
            _movieRepository.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _movieRepository.Update(movie);
            _movieRepository.SaveChanges();
        }

        public void Delete(Movie movie)
        {
            _movieRepository.Delete(movie);
            _movieRepository.SaveChanges();
        }
    }
}
