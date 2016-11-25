using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Contexts.Interfaces;
using Movies.Models;
using Movies.Repositories.Interfaces;

namespace Movies.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private static int counter = 1;

        private readonly IMoviesDbContextFactory _dbFactory;

        public MoviesRepository(IMoviesDbContextFactory dbFactory)
        {
            if (dbFactory == null)
            {
                throw new ArgumentNullException(nameof(dbFactory));
            }

            _dbFactory = dbFactory;
        }

        public async Task AddMovie(Movie movie)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            if (movie.Title == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine(counter++ + $": Adding movie title: {movie.Title}");

            using (var db = _dbFactory.Create())
            {
                var movieExists = db.Movies.SingleOrDefault(m => m.Title == movie.Title && m.Released == movie.Released);

                if (movieExists == null && movie.Title != null)
                {
                    try
                    {
                        db.Movies.Add(movie);

                        await db.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("O krv, wtf");
                    }
                }
            }
        }

        public ICollection<Movie> GetMoviesByGenre(string genre)
        {
            using (var db = _dbFactory.Create())
            {
                return db.Movies.Where(m => m.MovieGenres.All(mg => mg.Genre.Name == genre)).ToList();
            }
        }

        public Movie GetMovieByTitle(string title)
        {
            using (var db = _dbFactory.Create())
            {
                return db.Movies.SingleOrDefault(m => m.Title == title);
            }
        }

        public async Task AddGenres(ICollection<Genre> genres)
        {
            using (var db = _dbFactory.Create())
            {
                db.Genres.AddRange(genres);

                await db.SaveChangesAsync();
            }
        }

        public Genre GetGenre(string name)
        {
            using (var db = _dbFactory.Create())
            {
                return db.Genres.SingleOrDefault(g => g.Name == name);
            }
        }

        public Genre[] GetGenres()
        {
            using (var db = _dbFactory.Create())
            {
                return db.Genres.ToArray();
            }
        }
    }
}