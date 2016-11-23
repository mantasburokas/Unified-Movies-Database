using System;
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

		public Task<Genre> GetGenre(string name)
		{
			return _db.Genres.SingleOrDefaultAsync(g => g.Name == name);
		}
	}
}