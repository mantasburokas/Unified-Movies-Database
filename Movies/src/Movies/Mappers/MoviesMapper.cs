using System.Collections.Generic;
using Movies.Mappers.Interfaces;
using Movies.Models;
using Movies.Models.Dtos;
using Genre = Movies.Models.Dtos.Genre;

namespace Movies.Mappers
{
    public class MoviesMapper : IMoviesMapper
    {
        public ICollection<Movie> Map(ICollection<MovieByGenreTmdb> moviesByGenre)
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

        public Movie Map(MovieTmdb movieTmdb, MovieOmdb movieOmdb)
        {
            var movieGenres = new List<MovieGenre>();

            foreach (var genre in movieTmdb.Genres)
            {
                var movieGenre = new MovieGenre
                {
                    Title = movieOmdb.Title,
                    Released = movieOmdb.Released,
                    GenreId = genre.Id
                };

                movieGenres.Add(movieGenre);
            }

            var movie = new Movie
            {
                Title = movieOmdb.Title,
                Released = movieOmdb.Released,
                ImdbRating = movieOmdb.ImdbRating,
                Metascore = movieOmdb.Metascore,
                TomatoMeter = movieOmdb.TomatoMeter,
                MovieGenres = movieGenres
            };

            return movie;
        }

        public MovieTmdb Map(MovieByGenreTmdb movieByGenre)
        {
            var genres = new List<Genre>();

            foreach (var genre in movieByGenre.GenreIds)
            {
                var movieGenre = new Genre
                {
                    Id = genre
                };

                genres.Add(movieGenre);
            }

            var movie = new MovieTmdb
            {
                Genres = genres.ToArray()
            };

            return movie;
        }
    }
}