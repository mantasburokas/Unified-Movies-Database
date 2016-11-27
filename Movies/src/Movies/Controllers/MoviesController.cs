using System;
using Microsoft.AspNetCore.Mvc;
using Movies.Helpers;
using Movies.Services.Interfaces;

namespace Movies.Controllers
{
    [Route("api/movies")]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        private readonly IMoviesRequestsCache _requestsCache;

        public MoviesController(IMoviesService moviesService, IMoviesRequestsCache requestsCache)
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
        [Route("{title}", Name = nameof(MovieRoutes.GetMovieByTitle))]
        public IActionResult GetMovieByTitle(string title)
        {
            var movie = _moviesService.GetMovieByTitle(title);

            if (movie == null)
            {
                var requestAdded = _requestsCache.AddRequest(title);

                if (requestAdded)
                {
                    _moviesService.UpdateMoviesByTitle(title);

                    return NoContent();
                }

                if (_requestsCache.IsRequestFinished(title))
                {
                    return NotFound();
                }
            }

            return Ok(movie);
        }
    }
}