using System;
using System.Linq;
using System.Threading.Tasks;
using Movies.Clients.Interfaces;
using Movies.Contexts;
using Movies.Mappers.Interfaces;
using Movies.Services.Interfaces;

namespace Movies.Services
{
    public class GenresService : IGenresService
    {
        private readonly MoviesDbContext _db;

        private readonly ITmdbClient _client;

        private readonly IGenresMapper _mapper;

        public GenresService(MoviesDbContext db, ITmdbClient client, IGenresMapper mapper)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _db = db;
            _client = client;
            _mapper = mapper;
        }

        public async Task UpdateGenres()
        {
            var genres = await _client.GetGenres();

            if (genres != null)
            {
                var genresCollection = _mapper.Map(genres);

                _db.Genres.AddRange(genresCollection);
                
                await _db.SaveChangesAsync();
            }
        }
    }
}