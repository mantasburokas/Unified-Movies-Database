using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Clients.Interfaces
{
    public interface IOmdbClient
    {
        Task<Movie> GetMovieByTitle(string title);
    }
}