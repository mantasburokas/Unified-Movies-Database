using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Clients.Interfaces;
using Movies.Mappers.Interfaces;
using Movies.Models.Dtos;
using Movies.Repositories.Interfaces;
using Movies.Services.Interfaces;

namespace Movies.Services
{
    public class GenresService : IGenresService
    {
        private readonly IMoviesRepository _moviesRepository;

        private readonly ITmdbClient _client;

        private readonly IGenresMapper _mapper;

        public GenresService(IMoviesRepository moviesRepository, ITmdbClient client, IGenresMapper mapper)
        {
            if (moviesRepository == null)
            {
                throw new ArgumentNullException(nameof(moviesRepository));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _moviesRepository = moviesRepository;
            _client = client;
            _mapper = mapper;
            _moviesRepository = moviesRepository;
        }

        public async Task UpdateGenres()
        {
            var genres = await _client.GetGenres();

            if (genres != null)
            {
                var genresCollection = _mapper.Map(genres);

                await _moviesRepository.AddGenres(genresCollection);
            }
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            var genrePocos = await _moviesRepository.GetGenres();

            var genreDtos = _mapper.Map(genrePocos);

            return genreDtos;
        }
    }
}