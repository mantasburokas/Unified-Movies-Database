using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Repositories.Interfaces
{
	public interface IMoviesRepository
	{
		Task AddMovie(Movie movie);

		Task<Movie> GetMovieByTitle(string title);

		Task AddGenres(ICollection<Genre> genres);

		Task<Genre> GetGenre(string name);

		Task<Genre[]> GetGenres();
	}
}