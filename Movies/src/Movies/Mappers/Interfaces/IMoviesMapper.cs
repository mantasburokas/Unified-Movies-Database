using System.Collections.Generic;
using Movies.Models.Dtos;
using MovieDto = Movies.Models.Dtos.Movie;
using MoviePoco = Movies.Models.Pocos.Movie;

namespace Movies.Mappers.Interfaces
{
    public interface IMoviesMapper
    {
        MoviePoco Map(MovieTmdb movieTmdb, MovieOmdb movieOmdb);

        ICollection<MovieDto> Map(ICollection<MoviePoco> moviePocos);

        MovieDto Map(MoviePoco moviePoco);

        MovieTmdb Map(MovieByGenreTmdb movieByGenre);
    }
}