﻿using System;
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
                _moviesService.UpdateMoviesByTitle(title);

                return NoContent();
            }

            return Ok(movie);
        }
    }
}