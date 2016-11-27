using Movies.Models;
using Movies.Models.Dtos;

namespace Movies.Mappers.Interfaces
{
    public interface IMoviesMapper
    {
        Movie Map(MovieTmdb movieTmdb, MovieOmdb movieOmdb);

        MovieTmdb Map(MovieByGenreTmdb movieByGenre);
    }
}