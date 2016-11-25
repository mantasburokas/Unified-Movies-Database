using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Helpers;
using Movies.Services.Interfaces;

namespace Movies.Controllers
{
    [Route("api/genres")]
    public class GenresController : Controller
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            if (genresService == null)
            {
                throw new ArgumentNullException(nameof(genresService));
            }

            _genresService = genresService;
        }

        [HttpGet]
        [Route("", Name = nameof(GenreRoutes.GetGenres))]
        public IActionResult GetGenres()
        {
            var genres = _genresService.GetGenres();

            if (genres == null || !genres.Any())
            {
                #pragma warning disable 4014
                _genresService.UpdateGenres();
                #pragma warning restore 4014

                return NoContent();
            }

            return Ok(genres);
        }
    }
}
