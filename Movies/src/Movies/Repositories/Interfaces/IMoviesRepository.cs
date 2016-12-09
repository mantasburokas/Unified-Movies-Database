using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models.Entities;
using Movies.Models.Pocos;

namespace Movies.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        Task AddMovies(ICollection<Movie> movie);

        Task AddMovie(Movie movie);

        Task<ICollection<Movie>> GetMoviesByGenre(string genre);

        Task<ICollection<Movie>> GetMoviesByFilter(FilterParams parameters);
            
        Movie GetMovieByTitle(string title);

        Task AddGenres(ICollection<Genre> genres);

        Genre GetGenre(string name);

        Genre GetGenre(int id);

        Genre[] GetGenres();
    }
}