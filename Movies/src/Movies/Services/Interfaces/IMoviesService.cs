using System.Threading.Tasks;

namespace Movies.Services.Interfaces
{
    public interface IMoviesService
    {
        Task UpdateMoviesByTitle(string title);

        Task UpdateMoviesByGenre(string genre);
    }
}