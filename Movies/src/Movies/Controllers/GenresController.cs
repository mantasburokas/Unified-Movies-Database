using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Contexts;
using Movies.Helpers;
using Movies.Mappers.Interfaces;
using Movies.Services.Interfaces;

namespace Movies.Controllers
{
    [Route("api/genres")]
    public class GenresController : Controller
    {
        private readonly MoviesDbContext _db;

        private readonly IGenresService _genresService;

        private readonly IGenresMapper _mapper;

        public GenresController(MoviesDbContext db, IGenresService genresService, IGenresMapper mapper)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            if (genresService == null)
            {
                throw new ArgumentNullException(nameof(genresService));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _db = db;
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("", Name = nameof(GenreRoutes.GetGenres))]
        public IActionResult GetGenres()
        {
            var genres = _db.Genres.ToList();

            if (genres == null || !genres.Any())
            {
                Task.Factory.StartNew(() => _genresService.UpdateGenres());

                return NoContent();
            }

            var genreDtos = _mapper.Map(genres);

            return Ok(genreDtos);
        }
    }
}
