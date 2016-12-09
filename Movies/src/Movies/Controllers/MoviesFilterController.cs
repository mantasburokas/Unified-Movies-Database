using System;
using Microsoft.AspNetCore.Mvc;
using Movies.Models.Entities;
using Movies.Services.Interfaces;

namespace Movies.Controllers
{
    [Route("api/movies/filter")]
    public class MoviesFilterController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IMoviesRequestsCache _requestsCache;

        public MoviesFilterController(IMoviesService moviesService, IMoviesRequestsCache requestsCache)
        {
            if (moviesService == null)
            {
                throw new ArgumentNullException(nameof(moviesService));
            }

            if (requestsCache == null)
            {
                throw new ArgumentNullException(nameof(requestsCache));
            }

            _moviesService = moviesService;
            _requestsCache = requestsCache;
        }
        
        [HttpGet]
        public ActionResult GetFilteredMovies(int from, string genre, double imdb, int tomatometer, int metacritic, int votes)
        {
            var filterParams = new FilterParams(genre, votes, imdb, tomatometer, metacritic, from);

            var movies = _moviesService.GetMoviesByFilterParams(filterParams);

            var requestAdded = _requestsCache.AddRequest(genre);

            if (requestAdded)
            {
                _moviesService.UpdateMoviesByGenre(genre);
            }

            return Ok(movies);
        }
    }
}