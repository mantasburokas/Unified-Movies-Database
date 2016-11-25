using Microsoft.EntityFrameworkCore;
using Movies.Contexts.Interfaces;

namespace Movies.Contexts
{
    public class MoviesDbContextFactory : IMoviesDbContextFactory
    {
        private readonly string _connectionString;

        public MoviesDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MoviesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<MoviesDbContext>();

            options.UseSqlite(_connectionString);
            
            return new MoviesDbContext(options.Options);
        }
    }
}