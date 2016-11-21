using System.Threading.Tasks;
using Movies.Models.Dtos;

namespace Movies.Clients.Interfaces
{
    public interface ITmdbClient
    {
        Task<GenresCollection> GetGenres();

        Task<MoviesByGenreCollection> GetMoviesByGenre(int genreId);
    }
}