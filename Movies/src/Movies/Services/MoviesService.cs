using System;
using System.Threading.Tasks;
using Movies.Clients.Interfaces;
using Movies.Repositories.Interfaces;
using Movies.Services.Interfaces;

namespace Movies.Services
{
    public class MoviesService : IMoviesService
    {
	    private readonly IMoviesRepository _moviesRepository;

        private readonly IOmdbClient _omdbClient;

        private readonly ITmdbClient _tmdbClient;

        public MoviesService(IMoviesRepository moviesRepositorydb, IOmdbClient omdbClient, ITmdbClient tmdbClient)
        {
            if (moviesRepositorydb == null)
            {
                throw new ArgumentNullException(nameof(moviesRepositorydb));
            }

            if (omdbClient == null)
            {
                throw new ArgumentNullException(nameof(omdbClient));
            }

            if (tmdbClient == null)
            {
                throw new ArgumentNullException(nameof(tmdbClient));
            }

			_moviesRepository = moviesRepositorydb;
            _omdbClient = omdbClient;
            _tmdbClient = tmdbClient;
        }

        public async Task UpdateMoviesByTitle(string title)
        {
            var movie = await _omdbClient.GetMovieByTitle(title);

            if (movie != null)
            {
				await _moviesRepository.AddMovie(movie);
            }
        }

        public async Task UpdateMoviesByGenre(string genre)
        {
            var genreInstance = await _moviesRepository.GetGenre(genre);

            var movies = genreInstance != null ? await _tmdbClient.GetMoviesByGenre(genreInstance.GenreId) : null;

            if (movies != null)
            {
                
            }
        }
    }
}