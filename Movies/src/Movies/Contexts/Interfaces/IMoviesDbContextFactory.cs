namespace Movies.Contexts.Interfaces
{
    public interface IMoviesDbContextFactory
    {
        MoviesDbContext Create();
    }
}