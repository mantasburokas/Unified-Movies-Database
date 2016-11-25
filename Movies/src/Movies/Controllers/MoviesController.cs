using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Contexts;
using Movies.Helpers;
using Movies.Services.Interfaces;

namespace Movies.Controllers
{
    [Route("api/movies")]
    public class MoviesController : Controller
    {
        private readonly MoviesDbContext _db;

        private readonly IMoviesService _moviesService;

        private readonly IDictionary<string, MoviePoolingStates> _poolingRequests = new ConcurrentDictionary<string, MoviePoolingStates>();

        public MoviesController(MoviesDbContext db, IMoviesService moviesService)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            if (moviesService == null)
            {
                throw new ArgumentNullException(nameof(moviesService));
            }

            _db = db;
            _moviesService = moviesService;
        }

        [HttpGet]
        [Route("{title}", Name = nameof(MovieRoutes.GetMovieByTitle))]
        public IActionResult GetMovieByTitle(string title)
        {
            var movie = _db.Movies.SingleOrDefault(m => m.Title.ToLower() == title.ToLower());

            if (movie == null)
            {
                if (!_poolingRequests.ContainsKey(title))
                {
                    _poolingRequests.Add(title, MoviePoolingStates.Started);

                    Task.Factory.StartNew(() => _moviesService.UpdateMoviesByGenre(title));

                    return NoContent();
                }

                if (_poolingRequests[title] != MoviePoolingStates.Finished)
                {
                    return NoContent();
                }

                return NotFound();
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("{genre}", Name = nameof(MovieRoutes.GetMoviesByGenre))]
        public IActionResult GetMoviesByGenre(string genre)
        {
            var movies = _db.Movies.Where(m => m.MovieGenres.Any(mg => mg.Genre.Name == genre)).ToList();

            if (movies == null || !movies.Any())
            {
                _moviesService.UpdateMoviesByGenre(genre);

                return NoContent();
            }

            return Ok(movies);
        }
    }
}