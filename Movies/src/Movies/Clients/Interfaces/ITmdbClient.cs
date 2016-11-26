using System;
using System.Threading.Tasks;
using Movies.Models.Dtos;

namespace Movies.Clients.Interfaces
{
    public interface ITmdbClient
    {
        Task<GenresCollection> GetGenres();

        IObservable<MovieTmdb> GetMoviesByGenre(int genreId);

        Task<MoviesByImdbIdTmdb> GetMoviesByImdbId(string imdbId);
    }
}