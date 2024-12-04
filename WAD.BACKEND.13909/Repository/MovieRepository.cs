using Microsoft.EntityFrameworkCore;
using WAD.BACKEND._13909.DATA;
using WAD.BACKEND._13909.Models;

namespace WAD.BACKEND._13909.Repository
{
    public class MovieRepository:IMovieRepository
    {
        private readonly MovieAppDbContext _context;

        public MovieRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.Include(m => m.Category).ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
