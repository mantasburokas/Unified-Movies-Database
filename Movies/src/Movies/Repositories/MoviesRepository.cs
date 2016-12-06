using Microsoft.EntityFrameworkCore;
using Movies.Contexts.Interfaces;
using Movies.Models.Pocos;
using Movies.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IMoviesDbContextFactory _dbFactory;

        public MoviesRepository(IMoviesDbContextFactory dbFactory)
        {
            if (dbFactory == null)
            {
                throw new ArgumentNullException(nameof(dbFactory));
            }

            _dbFactory = dbFactory;
        }

        public async Task AddMovies(ICollection<Movie> movies)
        {
            using (var db = _dbFactory.Create())
            {
                foreach (var movie in movies)
                {
                    var movieExists = await db.Movies.AnyAsync(m => m.Title == movie.Title && m.Released == movie.Released);

                    if (!movieExists)
                    {
                        try
                        {
                            db.Movies.Add(movie);

                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public async Task AddMovie(Movie movie)
        {
            using (var db = _dbFactory.Create())
            {
                var movieExists = await db.Movies.AnyAsync(m => m.Title == movie.Title && m.Released == movie.Released);

                if (!movieExists && movie.Title != null)
                {
                    try
                    {
                        db.Movies.Add(movie);

                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public async Task<ICollection<Movie>> GetMoviesByGenre(string genreName)
        {
            using (var db = _dbFactory.Create())
            {
                ICollection<Movie> movies = null;

                try
                {
                    var genre = await db.Genres.SingleOrDefaultAsync(g => g.Name == genreName);

                    var genreId = genre?.GenreId;

                    genre = await db.Genres.Include(g => g.MovieGenres)
                        .ThenInclude(mg => mg.Movie)
                        .SingleOrDefaultAsync(g => g.GenreId == genreId);

                    movies = genre.MovieGenres.Select(mg => mg.Movie).ToList();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }

                return movies;
            }
        }

        public Movie GetMovieByTitle(string title)
        {
            using (var db = _dbFactory.Create())
            {
                Movie movie = null;

                try
                {
                    movie = db.Movies.SingleOrDefault(m => m.Title == title);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }

                return movie;
            }
        }

        public async Task AddGenres(ICollection<Genre> genres)
        {
            using (var db = _dbFactory.Create())
            {
                foreach (var genre in genres)
                {
                    var genreExists = await db.Genres.AnyAsync(g => g.GenreId == genre.GenreId);

                    if (!genreExists)
                    {
                        db.Genres.Add(genre);

                        await db.SaveChangesAsync();
                    }
                }
            }
        }

        public Genre GetGenre(string name)
        {
            Genre genre = null;

            using (var db = _dbFactory.Create())
            {
                try
                {
                    genre = db.Genres.SingleOrDefault(g => g.Name == name);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }

            return genre;
        }

        public Genre GetGenre(int id)
        {
            using (var db = _dbFactory.Create())
            {
                return db.Genres.SingleOrDefault(g => g.GenreId == id);
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