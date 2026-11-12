using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class MovieCommentRepository : IMovieCommentRepository
    {
        private readonly AppDbContext _context;

        public MovieCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<MovieComment> GetByMovieId(int movieId) =>
            _context.MovieComments.Where(c => c.MovieId == movieId).OrderBy(c => c.CreatedAt).ToList();

        public MovieComment GetById(int id) => _context.MovieComments.FirstOrDefault(c => c.Id == id);

        public void Add(MovieComment comment) => _context.MovieComments.Add(comment);

        public void Update(MovieComment comment) => _context.MovieComments.Update(comment);

        public void Delete(MovieComment comment) => _context.MovieComments.Remove(comment);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
