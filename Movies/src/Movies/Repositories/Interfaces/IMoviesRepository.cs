using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Repositories.Interfaces
{
	public interface IMoviesRepository
	{
		Task<int> AddMovie(Movie movie);

		Task<int> AddGenres(ICollection<Genre> genres);

		Task<Genre> GetGenre(string name);

		Task<Genre[]> GetGenres();
	}
}