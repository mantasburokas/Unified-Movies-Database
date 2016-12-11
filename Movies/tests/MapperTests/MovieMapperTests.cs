using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Movies.Mappers;
using Movies.Models.Dtos;
using Xunit;
using Genre = Movies.Models.Dtos.Genre;
using MoviePoco = Movies.Models.Pocos.Movie;

namespace MapperTests
{
    public class MovieMapperTests
    {
        #region ClassData

        public class ValidationDataOmdbTmdb : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>();

            public ValidationDataOmdbTmdb()
            {
                _data.Add(new object[] {
                new MovieOmdb
                {
                    Title = "Title",
                    ImdbVotes = "Votes",
                    ImdbRating = "ImdbRating",
                    Awards = "Awards",
                    Metascore = "Metascore",
                    Director = "Director",
                    TomatoMeter = "Tomatometer",
                    Runtime = "Runtime",
                    Plot = "Plot",
                    ImdbId = "ImdbId",
                    Released = "Released"
                },
                new MovieTmdb
                {
                    ImdbId = "ImdbId",
                    Genres = new [] {new Genre {Id = 1, Name = "Genre"} }
                }
                });
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class ValidationDataPoco : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>();

            public ValidationDataPoco()
            {
                _data.Add(new object[]
                {
                    new MoviePoco
                    {
                        Title = "Title",
                        ImdbVotes = "Votes",
                        ImdbRating = "ImdbRating",
                        Awards = "Awards",
                        Metascore = "Metascore",
                        Director = "Director",
                        TomatoMeter = "Tomatometer",
                        Runtime = "Runtime",
                        Plot = "Plot",
                        Released = "Released"
                    }
                });
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class ValidationDataTmdb : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>();

            public ValidationDataTmdb()
            {
                _data.Add(new object[] {
                    new MovieByGenreTmdb
                    {
                        GenreIds =  new List<int> {1}
                    }
                });
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class ValidationDataPocoCollection : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>();

            public ValidationDataPocoCollection()
            {
                _data.Add(new object[]
                {
                    new List<MoviePoco> {
                        new MoviePoco
                        {
                            Title = "Title",
                            ImdbVotes = "Votes",
                            ImdbRating = "ImdbRating",
                            Awards = "Awards",
                            Metascore = "Metascore",
                            Director = "Director",
                            TomatoMeter = "Tomatometer",
                            Runtime = "Runtime",
                            Plot = "Plot",
                            Released = "Released"
                        }
                    }
                });
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #endregion

        [Theory, ClassData(typeof(ValidationDataOmdbTmdb))]
        public void ItReturns_CorrectPoco_FromTmdbOmdbDtos(MovieOmdb omdb, MovieTmdb tmdb)
        {
            var moviesMapper = new MoviesMapper();

            var moviesPoco = moviesMapper.Map(tmdb, omdb);

            bool movieGenresMappedCorrectly = true;

            for (var i = 0; i < tmdb.Genres.Length; i++)
            {
                var movieGenre = moviesPoco.MovieGenres.ElementAt(i);

                if (movieGenre.GenreId != tmdb.Genres[i].Id ||
                    movieGenre.Title != omdb.Title ||
                    movieGenre.Released != omdb.Released)
                {
                    movieGenresMappedCorrectly = false;
                }
            }


            Assert.True(movieGenresMappedCorrectly);
            Assert.Equal(omdb.ImdbVotes, moviesPoco.ImdbVotes);
            Assert.Equal(omdb.Awards, moviesPoco.Awards);
            Assert.Equal(omdb.Director, moviesPoco.Director);
            Assert.Equal(omdb.ImdbRating, moviesPoco.ImdbRating);
            Assert.Equal(omdb.Metascore, moviesPoco.Metascore);
            Assert.Equal(omdb.Plot, moviesPoco.Plot);
            Assert.Equal(omdb.Released, moviesPoco.Released);
            Assert.Equal(omdb.Runtime, moviesPoco.Runtime);
            Assert.Equal(omdb.Title, moviesPoco.Title);
            Assert.Equal(omdb.TomatoMeter, moviesPoco.TomatoMeter);
        }

        [Theory, ClassData(typeof(ValidationDataPoco))]
        public void ItReturns_CorrectMovieDto_FromMoviePoco(MoviePoco movie)
        {
            var moviesMapper = new MoviesMapper();

            var movieDto = moviesMapper.Map(movie);

            Assert.Equal(movie.ImdbVotes, movieDto.ImdbVotes);
            Assert.Equal(movie.Awards, movieDto.Awards);
            Assert.Equal(movie.Director, movieDto.Director);
            Assert.Equal(movie.ImdbRating, movieDto.ImdbRating);
            Assert.Equal(movie.Metascore, movieDto.Metascore);
            Assert.Equal(movie.Plot, movieDto.Plot);
            Assert.Equal(movie.Released, movieDto.Released);
            Assert.Equal(movie.Runtime, movieDto.Runtime);
            Assert.Equal(movie.Title, movieDto.Title);
            Assert.Equal(movie.TomatoMeter, movieDto.TomatoMeter);
        }

        [Theory, ClassData(typeof(ValidationDataTmdb))]
        public void ItReturns_CorrectTmdbMovie_FromMovieByGenreTmdb(MovieByGenreTmdb movie)
        {
            var moviesMapper = new MoviesMapper();

            var movieTmdb = moviesMapper.Map(movie);

            var correctTmdb = true;

            for (var i = 0; i < movieTmdb.Genres.Length; i++)
            {
                var genre = movieTmdb.Genres.ElementAt(i);

                if (genre.Id != movie.GenreIds.ElementAt(i))
                {
                    correctTmdb = false;
                }
            }

            Assert.True(correctTmdb);
        }

        [Theory, ClassData(typeof(ValidationDataPocoCollection))]
        public void ItReturns_CorrectMovies_FromPocos(ICollection<MoviePoco> movies)
        {
            var moviesMapper = new MoviesMapper();

            var moviesDto = moviesMapper.Map(movies);

            for(var i = 0; i < movies.Count; i++)
            {
                Assert.Equal(moviesDto.ElementAt(i).ImdbVotes, movies.ElementAt(i).ImdbVotes);
                Assert.Equal(moviesDto.ElementAt(i).Awards, movies.ElementAt(i).Awards);
                Assert.Equal(moviesDto.ElementAt(i).Director, movies.ElementAt(i).Director);
                Assert.Equal(moviesDto.ElementAt(i).ImdbRating, movies.ElementAt(i).ImdbRating);
                Assert.Equal(moviesDto.ElementAt(i).Metascore, movies.ElementAt(i).Metascore);
                Assert.Equal(moviesDto.ElementAt(i).Plot, movies.ElementAt(i).Plot);
                Assert.Equal(moviesDto.ElementAt(i).Released, movies.ElementAt(i).Released);
                Assert.Equal(moviesDto.ElementAt(i).Runtime, movies.ElementAt(i).Runtime);
                Assert.Equal(moviesDto.ElementAt(i).Title, movies.ElementAt(i).Title);
                Assert.Equal(moviesDto.ElementAt(i).TomatoMeter, movies.ElementAt(i).TomatoMeter);
            }
        }
    }
}
