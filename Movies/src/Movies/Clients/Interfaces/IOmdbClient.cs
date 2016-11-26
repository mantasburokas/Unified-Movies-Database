using System.Threading.Tasks;
using Movies.Models.Dtos;

namespace Movies.Clients.Interfaces
{
    public interface IOmdbClient
    {
        Task<MovieOmdb> GetMovieByTitle(string title);

        Task<MovieOmdb> GetMovieByImdbId(string id);
    }
}