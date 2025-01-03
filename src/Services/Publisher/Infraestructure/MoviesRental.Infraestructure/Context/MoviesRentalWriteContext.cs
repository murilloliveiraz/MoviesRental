using Microsoft.EntityFrameworkCore;
using MoviesRental.Domain.Entities;

namespace MoviesRental.Infraestructure.Context
{
    public class MoviesRentalWriteContext : DbContext
    {
        public MoviesRentalWriteContext(){}

        public MoviesRentalWriteContext(DbContextOptions<MoviesRentalWriteContext> options): base(options) { }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Dvd> Dvds { get; set; }
    }
}
