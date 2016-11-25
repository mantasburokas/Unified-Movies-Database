using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Services.Interfaces
{
    public interface IMoviesService
    {
        Task UpdateMoviesByTitle(string title);

        Movie GetMovieByTitle(string title);

        Task UpdateMoviesByGenre(string genre);

        ICollection<Movie> GetMoviesByGenre(string genre);
    }
}