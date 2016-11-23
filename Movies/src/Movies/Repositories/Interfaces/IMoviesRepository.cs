using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Repositories.Interfaces
{
	public interface IMoviesRepository
	{
		Task<int> AddMovie(Movie movie);

		Task GetGenre(string name);
	}
}