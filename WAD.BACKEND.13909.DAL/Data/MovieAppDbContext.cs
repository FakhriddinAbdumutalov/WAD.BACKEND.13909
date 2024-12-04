using Microsoft.EntityFrameworkCore;
using WAD.BACKEND._13909.Models;

namespace WAD.BACKEND._13909.DATA
{
    public class MovieAppDbContext:DbContext
    {
        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
