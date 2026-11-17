using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class MovieService(IMovieRepository _movieRepository)
    {
        public Task<List<Movie>> GetAllAsync() => _movieRepository.GetAllAsync();

        public Task<Movie> GetByIdAsync(int id) => _movieRepository.GetByIdAsync(id);

        public async Task AddAsync(Movie movie)
        {
            _movieRepository.Add(movie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _movieRepository.Update(movie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            _movieRepository.Delete(movie);
            await _movieRepository.SaveChangesAsync();
        }
    }
}
