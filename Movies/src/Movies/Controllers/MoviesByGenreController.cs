﻿using System;
using Microsoft.AspNetCore.Mvc;
using Movies.Helpers;
using Movies.Services.Interfaces;

namespace Movies.Controllers
{
    [Route("api/movies/genre")]
    public class MoviesByGenreController : Controller
    {
        private readonly IMoviesService _moviesService;

        private readonly IMoviesRequestsCache _requestsCache;

        public MoviesByGenreController(IMoviesService moviesService, IMoviesRequestsCache requestsCache)
        {
            if (moviesService == null)
            {
                throw new ArgumentNullException(nameof(moviesService));
            }

            _moviesService = moviesService;
            _requestsCache = requestsCache;
        }

        [HttpGet]
        [Route("{genre}", Name = nameof(MovieRoutes.GetMoviesByGenre))]
        public IActionResult GetMoviesByGenre(string genre)
        {
            var movies = _moviesService.GetMoviesByGenre(genre);

            var requestAdded = _requestsCache.AddRequest(genre);
                
            if (requestAdded)
            {
                _moviesService.UpdateMoviesByGenre(genre);

                return Ok(movies);
            }

            if (_requestsCache.IsRequestFinished(genre))
            {
                return StatusCode(404, movies);
            }

            return Ok(movies);
        }
    }
}