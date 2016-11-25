using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        Task AddMovie(Movie movie);

        ICollection<Movie> GetMoviesByGenre(string genre);
            
        Movie GetMovieByTitle(string title);

        Task AddGenres(ICollection<Genre> genres);

        Genre GetGenre(string name);

        Genre[] GetGenres();
    }
}