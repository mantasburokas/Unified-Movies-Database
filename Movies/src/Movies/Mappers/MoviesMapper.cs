using Movies.Mappers.Interfaces;
using Movies.Models.Dtos;
using Movies.Models.Pocos;
using System.Collections.Generic;
using Genre = Movies.Models.Dtos.Genre;
using MovieDto = Movies.Models.Dtos.Movie;
using MoviePoco = Movies.Models.Pocos.Movie;

namespace Movies.Mappers
{
    public class MoviesMapper : IMoviesMapper
    {
        public MoviePoco Map(MovieTmdb movieTmdb, MovieOmdb movieOmdb)
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

            var movie = new MoviePoco
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

        public ICollection<MovieDto> Map(ICollection<MoviePoco> moviePocos)
        {
            var movies = new List<MovieDto>();

            foreach (var movie in moviePocos)
            {
                var movieDto = Map(movie);

                movies.Add(movieDto);
            }

            return movies;
        }

        public MovieDto Map(MoviePoco moviePoco)
        {
            var movie = new MovieDto
            {
                Title = moviePoco.Title,
                Released = moviePoco.Released,
                ImdbRating = moviePoco.ImdbRating,
                Metascore = moviePoco.Metascore,
                TomatoMeter = moviePoco.TomatoMeter
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