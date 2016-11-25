using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Helpers;
using Movies.Services.Interfaces;

namespace Movies.Controllers
{
    [Route("api/movies")]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            if (moviesService == null)
            {
                throw new ArgumentNullException(nameof(moviesService));
            }

            _moviesService = moviesService;
        }

        [HttpGet]
        [Route("{title}", Name = nameof(MovieRoutes.GetMovieByTitle))]
        public IActionResult GetMovieByTitle(string title)
        {
            var movie = _moviesService.GetMovieByTitle(title);

            if (movie == null)
            {
                Task.Factory.StartNew(() => _moviesService.UpdateMoviesByGenre(title));

                return NoContent();
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("{genre}", Name = nameof(MovieRoutes.GetMoviesByGenre))]
        public IActionResult GetMoviesByGenre(string genre)
        {
            var movies = _moviesService.GetMoviesByGenre(genre);

            if (movies == null || !movies.Any())
            {
                _moviesService.UpdateMoviesByGenre(genre);

                return NoContent();
            }

            return Ok(movies);
        }
    }
}