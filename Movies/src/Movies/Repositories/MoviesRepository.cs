using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Contexts;
using Movies.Models;
using Movies.Repositories.Interfaces;

namespace Movies.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly MoviesDbContext _db;

        private static int counter = 1;

        public MoviesRepository(MoviesDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            _db = db;
        }

        public async Task AddMovie(Movie movie)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            if (movie.Title == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine(counter++ + $": Adding movie title: {movie.Title}");

            var movieExists = await _db.Movies.
                SingleOrDefaultAsync(m => m.Title == movie.Title && m.Released == movie.Released);

            if (movieExists == null && movie.Title != null)
            {
                try
                {
                    _db.Movies.Add(movie);

                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("O krv, wtf");
                }
            }
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            return await _db.Movies.SingleOrDefaultAsync(m => m.Title == title);
        }

        public async Task AddGenres(ICollection<Genre> genres)
        {
            _db.Genres.AddRange(genres);

            await _db.SaveChangesAsync();
        }

        public Task<Genre> GetGenre(string name)
        {
            return _db.Genres.SingleOrDefaultAsync(g => g.Name == name);
        }

        public Task<Genre[]> GetGenres()
        {
            return _db.Genres.ToArrayAsync();
        }
    }
}