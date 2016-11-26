using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Contexts.Interfaces;
using Movies.Models;
using Movies.Repositories.Interfaces;

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
                        db.Movies.Add(movie);

                        await db.SaveChangesAsync();
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
                    db.Movies.Add(movie);

                    await db.SaveChangesAsync();
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
            using (var db = _dbFactory.Create())
            {
                return db.Genres.SingleOrDefault(g => g.Name == name);
            }
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