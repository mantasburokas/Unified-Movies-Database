using Movies.Clients.Interfaces;
using Movies.Mappers.Interfaces;
using Movies.Models;
using Movies.Models.Dtos;
using Movies.Repositories.Interfaces;
using Movies.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Movies.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;

        private readonly IOmdbClient _omdbClient;

        private readonly ITmdbClient _tmdbClient;

        private readonly IMoviesMapper _moviesMapper;

        public MoviesService(IMoviesRepository moviesRepository,
            IOmdbClient omdbClient,
            ITmdbClient tmdbClient,
            IMoviesMapper moviesMapper)
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

            if (moviesMapper == null)
            {
                throw new ArgumentNullException(nameof(moviesMapper));
            }

            _moviesRepository = moviesRepository;
            _omdbClient = omdbClient;
            _tmdbClient = tmdbClient;
            _moviesMapper = moviesMapper;
        }

        public async Task UpdateMoviesByTitle(string title)
        {
            var movieOmdb = await _omdbClient.GetMovieByTitle(title);

            if (movieOmdb != null)
            {
                var moviesTmdb = await _tmdbClient.GetMoviesByImdbId(movieOmdb.ImdbId);

                var movies = new List<Movie>();

                foreach (var movieByGenre in moviesTmdb.MoviesByGenre)
                {
                    var movieTmdb = _moviesMapper.Map(movieByGenre);

                    var movie = _moviesMapper.Map(movieTmdb, movieOmdb);

                    movies.Add(movie);
                }

                await _moviesRepository.AddMovies(movies);
            }
        }

        public Movie GetMovieByTitle(string title)
        {
            return _moviesRepository.GetMovieByTitle(title);
        }

        protected async Task UpdateMoviesByImdbId(MovieTmdb movieTmdb)
        {
            var movieOmdb = await _omdbClient.GetMovieByImdbId(movieTmdb.ImdbId);

            if (movieOmdb != null)
            {
                var movie = _moviesMapper.Map(movieTmdb, movieOmdb);

                RemoveUnkownGenres(movie);

                await _moviesRepository.AddMovie(movie);
            }
        }

        protected void RemoveUnkownGenres(Movie movie)
        {
            movie.MovieGenres = movie.MovieGenres.Where(mg => _moviesRepository.GetGenre(mg.GenreId) != null).ToList();
        }

        public async Task UpdateMoviesByGenre(string genre)
        {
            var genreInstance = _moviesRepository.GetGenre(genre);

            if (genreInstance != null)
            {
                _tmdbClient.GetMoviesByGenre(genreInstance.GenreId)
                    .Do(async movieTmdb => await UpdateMoviesByImdbId(movieTmdb))
                    .Subscribe();
            }
        }

        public ICollection<Movie> GetMoviesByGenre(string genre)
        {
            return _moviesRepository.GetMoviesByGenre(genre);
        }
    }
}