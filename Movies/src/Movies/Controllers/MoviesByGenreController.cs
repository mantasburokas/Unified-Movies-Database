using System;
using Microsoft.AspNetCore.Mvc;
using Movies.Helpers;
using Movies.Services.Interfaces;
using System.Linq;

namespace Movies.Controllers
{
    public class MoviesByGenreController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesByGenreController(IMoviesService moviesService)
        {
            if (moviesService == null)
            {
                throw new ArgumentNullException(nameof(moviesService));
            }

            _moviesService = moviesService;
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