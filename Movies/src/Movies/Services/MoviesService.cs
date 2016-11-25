using System;
using System.Reactive.Linq;
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

        public MoviesService(IMoviesRepository moviesRepository, IOmdbClient omdbClient, ITmdbClient tmdbClient)
        {
            if (moviesRepository == null)
            {
                throw new ArgumentNullException(nameof(moviesRepository));
            }

            if (omdbClient == null)
            {
                throw new ArgumentNullException(nameof(omdbClient));
            }

            if (tmdbClient == null)
            {
                throw new ArgumentNullException(nameof(tmdbClient));
            }

            _moviesRepository = moviesRepository;
            _omdbClient = omdbClient;
            _tmdbClient = tmdbClient;
        }

        public async Task UpdateMoviesByTitle(string title)
        {
            Console.WriteLine(title);

            var movie = await _omdbClient.GetMovieByTitle(title);

            if (movie != null)
            {
                await _moviesRepository.AddMovie(movie);
            }
        }

        protected async Task UpdateMoviesByImdbId(string id)
        {
            var movie = await _omdbClient.GetMovieByImdbId(id);

            if (movie != null)
            {
                await _moviesRepository.AddMovie(movie);
            }
        }

        public async Task UpdateMoviesByGenre(string genre)
        {
            var genreInstance = await _moviesRepository.GetGenre(genre);

            if (genreInstance != null)
            {
                var moviesByGenreObservable = _tmdbClient.MoviesTmdbObservable;

                moviesByGenreObservable
                    .Do( movieTmdb =>  UpdateMoviesByImdbId(movieTmdb.ImdbId).GetAwaiter().GetResult())
                    .Subscribe();

                await _tmdbClient.GetMoviesByGenre(genreInstance.GenreId);
            }
        }
    }
}