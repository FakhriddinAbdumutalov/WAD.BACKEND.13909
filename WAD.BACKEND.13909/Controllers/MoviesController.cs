using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.BACKEND._13909.DATA;
using WAD.BACKEND._13909.Models;

namespace WAD.BACKEND._13909.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieAppDbContext _context;

        public MoviesController(MovieAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies
                                 .Include(m => m.Category)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies
                                       .Include(m => m.Category)
                                       .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            var category = await _context.Categories.FindAsync(movie.CategoryId);
            if (category == null)
            {
                return BadRequest(new { message = "Invalid CategoryId. Category does not exist." });
            }

            movie.Category = category;
            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            var category = await _context.Categories.FindAsync(movie.CategoryId);
            if (category == null)
            {
                return BadRequest(new { message = "Invalid CategoryId. Category does not exist." });
            }

            movie.Category = category;
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var createdMovie = await _context.Movies
                                              .Include(m => m.Category)
                                              .FirstOrDefaultAsync(m => m.Id == movie.Id);

            return CreatedAtAction("GetMovie", new { id = createdMovie.Id }, createdMovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
