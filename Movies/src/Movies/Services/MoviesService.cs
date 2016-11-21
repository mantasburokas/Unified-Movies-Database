using System;
using System.Linq;
using System.Threading.Tasks;
using Movies.Clients;
using Movies.Clients.Interfaces;
using Movies.Contexts;
using Movies.Services.Interfaces;

namespace Movies.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesDbContext _db;

        private readonly IOmdbClient _omdbClient;

        private readonly ITmdbClient _tmdbClient;

        public MoviesService(MoviesDbContext db, IOmdbClient omdbClient, ITmdbClient tmdbClient)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            if (omdbClient == null)
            {
                throw new ArgumentNullException(nameof(omdbClient));
            }

            if (tmdbClient == null)
            {
                throw new ArgumentNullException(nameof(tmdbClient));
            }

            _db = db;
            _omdbClient = omdbClient;
            _tmdbClient = tmdbClient;
        }

        public async Task UpdateMoviesByTitle(string title)
        {
            var movie = await _omdbClient.GetMovieByTitle(title);

            if (movie != null)
            {
                _db.Movies.Add(movie);

                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateMoviesByGenre(string genre)
        {
            var genreInstance = _db.Genres.SingleOrDefault(g => g.Name == genre);

            var movies = genreInstance != null ? await _tmdbClient.GetMoviesByGenre(genreInstance.GenreId) : null;

            if (movies != null)
            {
                
            }
        }
    }
}