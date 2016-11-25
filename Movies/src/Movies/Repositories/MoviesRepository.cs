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

		public MoviesRepository(MoviesDbContext db)
		{
			if (db == null)
			{
				throw new ArgumentNullException(nameof(db));
			}

			_db = db;
		}

		public Task<int> AddMovie(Movie movie)
		{
			_db.Movies.Add(movie);

			return _db.SaveChangesAsync();
		}

		public Task<int> AddGenres(ICollection<Genre> genres)
		{
			_db.Genres.AddRange(genres);

			return _db.SaveChangesAsync();
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