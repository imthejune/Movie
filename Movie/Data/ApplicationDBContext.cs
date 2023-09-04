using Microsoft.EntityFrameworkCore;
using Movie.Models;

namespace Movie.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options) { 
        
        }

        public DbSet<MovieList> Movies { get; set; }
    }
}
