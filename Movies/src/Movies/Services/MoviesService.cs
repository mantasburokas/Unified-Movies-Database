using Movies.Clients.Interfaces;
using Movies.Mappers.Interfaces;
using Movies.Models.Dtos;
using Movies.Repositories.Interfaces;
using Movies.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MovieDto = Movies.Models.Dtos.Movie;
using MoviePoco = Movies.Models.Pocos.Movie;

namespace Movies.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;

        private readonly IOmdbClient _omdbClient;

        private readonly ITmdbClient _tmdbClient;

        private readonly IMoviesMapper _moviesMapper;

        private readonly IMoviesRequestsCache _requestsCache;

        public MoviesService(IMoviesRepository moviesRepository,
            IOmdbClient omdbClient,
            ITmdbClient tmdbClient,
            IMoviesMapper moviesMapper,
            IMoviesRequestsCache requestsCache)
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

            if (requestsCache == null)
            {
                throw new ArgumentNullException(nameof(requestsCache));
            }

            _moviesRepository = moviesRepository;
            _omdbClient = omdbClient;
            _tmdbClient = tmdbClient;
            _moviesMapper = moviesMapper;
            _requestsCache = requestsCache;
        }

        public async Task UpdateMoviesByTitle(string title)
        {
            var movieOmdb = await _omdbClient.GetMovieByTitle(title);

            if (movieOmdb != null && movieOmdb.ImdbId != null)
            {
                var moviesTmdb = await _tmdbClient.GetMoviesByImdbId(movieOmdb.ImdbId);

                var movies = new List<MoviePoco>();

                foreach (var movieByGenre in moviesTmdb.MoviesByGenre)
                {
                    var movieTmdb = _moviesMapper.Map(movieByGenre);

                    var movie = _moviesMapper.Map(movieTmdb, movieOmdb);

                    movies.Add(movie);
                }

                if (movies.Any())
                { 
                    await _moviesRepository.AddMovies(movies);
                }
            }

            _requestsCache.FinishRequest(title);
        }

        public MovieDto GetMovieByTitle(string title)
        {
            var movie =  _moviesRepository.GetMovieByTitle(title);

            MovieDto movieDto = null;

            if (movie != null)
            {
                movieDto = _moviesMapper.Map(movie);
            }

            return movieDto;
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

        protected void RemoveUnkownGenres(MoviePoco movie)
        {
            movie.MovieGenres = movie.MovieGenres.Where(mg => _moviesRepository.GetGenre(mg.GenreId) != null).ToList();
        }

        public async Task UpdateMoviesByGenre(string genre)
        {
            var genreInstance = _moviesRepository.GetGenre(genre);

            if (genreInstance != null)
            {
                _tmdbClient.GetMoviesByGenre(genreInstance.GenreId)
                    .Do(async movieTmdb => await UpdateMoviesByImdbId(movieTmdb),
                        () => _requestsCache.FinishRequest(genre))
                    .Subscribe();
            }
        }

        public ICollection<MovieDto> GetMoviesByGenre(string genre)
        {
            var movies = _moviesRepository.GetMoviesByGenre(genre).Result;

            ICollection<MovieDto> movieDtos = null;

            if (movies != null && movies.Any())
            {
                movieDtos = _moviesMapper.Map(movies);
            }

            return movieDtos;
        }
    }
}