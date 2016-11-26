using System.Collections.Generic;
using Movies.Models;
using Movies.Models.Dtos;

namespace Movies.Mappers.Interfaces
{
    public interface IMoviesMapper
    {
        ICollection<Movie> Map(ICollection<MovieByGenreTmdb> moviesByGenre);

        Movie Map(MovieTmdb movieTmdb, MovieOmdb movieOmdb);

        MovieTmdb Map(MovieByGenreTmdb movieByGenre);
    }
}