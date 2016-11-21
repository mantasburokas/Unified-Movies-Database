using System.Collections.Generic;
using Movies.Mappers.Interfaces;
using Movies.Models.Dtos;
using GenrePoco = Movies.Models.Genre;
using GenreDto = Movies.Models.Dtos.Genre;

namespace Movies.Mappers
{
    public class GenresMapper : IGenresMapper
    {
        public ICollection<GenrePoco> Map(GenresCollection genres)
        {
            var genresList = new List<GenrePoco>();

            foreach (var genre in genres.Genres)
            {
                genresList.Add(new GenrePoco
                {
                    GenreId = genre.Id,
                    Name = genre.Name
                });
            }

            return genresList;
        }

        public ICollection<Genre> Map(ICollection<GenrePoco> genres)
        {
            var genresList = new List<GenreDto>();

            foreach (var genre in genres)
            {
                genresList.Add(new GenreDto
                {
                    Id = genre.GenreId,
                    Name = genre.Name
                });
            }

            return genresList;
        }
    }
}