using System;
using System.Threading.Tasks;
using Movies.Models.Dtos;

namespace Movies.Clients.Interfaces
{
    public interface ITmdbClient
    {
        IObservable<MovieTmdb> MoviesTmdbObservable { get; }

        Task<GenresCollection> GetGenres();

        Task GetMoviesByGenre(int genreId);
    }
}