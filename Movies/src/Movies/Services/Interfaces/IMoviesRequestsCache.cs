namespace Movies.Services.Interfaces
{
    public interface IMoviesRequestsCache
    {
        bool AddRequest(string requestName);

        bool FinishRequest(string requestName);

        bool IsRequestFinished(string requestName);
    }
}