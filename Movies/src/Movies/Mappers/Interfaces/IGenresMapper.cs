using System.Collections.Generic;
using Movies.Models.Dtos;
using GenrePoco = Movies.Models.Genre;
using GenreDto = Movies.Models.Dtos.Genre;

namespace Movies.Mappers.Interfaces
{
    public interface IGenresMapper
    {
        ICollection<GenrePoco> Map(GenresCollection genres);
        ICollection<GenreDto> Map(ICollection<GenrePoco> genres);
    }
}