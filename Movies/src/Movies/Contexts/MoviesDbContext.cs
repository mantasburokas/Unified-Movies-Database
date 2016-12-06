using Microsoft.EntityFrameworkCore;
using Movies.Models.Pocos;

namespace Movies.Contexts
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasKey(m => new { m.Title, m.Released });

            #region Many-to-Many Movie Genre

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new
                {
                    mg.Title,
                    mg.Released,
                    mg.GenreId
                });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => new
                {
                    mg.Title,
                    mg.Released
                });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => new
                {
                    mg.GenreId
                });

            #endregion

            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.ImdbRating)
                .IsUnique(false);

            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.Metascore)
                .IsUnique(false);

            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.TomatoMeter)
                .IsUnique(false);

            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.Name)
                .IsUnique();
        }
    }
}