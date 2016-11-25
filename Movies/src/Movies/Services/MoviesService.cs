using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Movies.Clients.Interfaces;
using Movies.Models;
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

        public Movie GetMovieByTitle(string title)
        {
            return _moviesRepository.GetMovieByTitle(title);
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
            var genreInstance = _moviesRepository.GetGenre(genre);

            if (genreInstance != null)
            {
                var moviesByGenreObservable = _tmdbClient.MoviesTmdbObservable;

                moviesByGenreObservable
                    .Do(movieTmdb => UpdateMoviesByImdbId(movieTmdb.ImdbId))
                    .Subscribe();

                await _tmdbClient.GetMoviesByGenre(genreInstance.GenreId);
            }
        }

        public ICollection<Movie> GetMoviesByGenre(string genre)
        {
            return _moviesRepository.GetMoviesByGenre(genre);
        }
    }
}