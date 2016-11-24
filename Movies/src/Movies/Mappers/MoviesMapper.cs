using System.Collections.Generic;
using Movies.Mappers.Interfaces;
using Movies.Models;
using Movies.Models.Dtos;

namespace Movies.Mappers
{
    public class MoviesMapper : IMoviesMapper
    {
        public ICollection<Movie> Map(ICollection<MovieByGenre> moviesByGenre)
        {
            var movies = new List<Movie>();

            foreach (var movieByGenre in moviesByGenre)
            {
                var movieGenres = new List<MovieGenre>();

                foreach (var genreId in movieByGenre.GenreIds)
                {
                    var movieGenre = new MovieGenre
                    {
                        Title = movieByGenre.Title,
                        Released = movieByGenre.ReleaseDate,
                        GenreId = genreId
                    };

                    movieGenres.Add(movieGenre);
                }

                var movie = new Movie
                {
                    Title = movieByGenre.Title,
                    Released = movieByGenre.ReleaseDate,
                    MovieGenres = movieGenres
                };

                movies.Add(movie);
            }

            return movies;
        }   
    }
}