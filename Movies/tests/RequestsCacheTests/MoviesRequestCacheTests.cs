using Movies.Services;
using Xunit;

namespace RequestsCacheTests
{
    public class MoviesRequestCacheTests
    {
        [Theory, InlineData("request")]
        public void ItReturnsTrue_WhenRequestAdded(string request)
        {
            Assert.True(new MoviesRequestsCache().AddRequest(request));
        }

        [Theory, InlineData("request")]
        public void ItReturnsFalse_WhenRequestAlreadyAdded(string request)
        {
            var cache = new MoviesRequestsCache();

            cache.AddRequest(request);

            Assert.False(cache.AddRequest(request));
        }

        [Theory, InlineData("request")]
        public void ItReturnsFalse_WhenNoRequestToFinish(string request)
        {
            var cache = new MoviesRequestsCache();

            Assert.False(cache.FinishRequest(request));
        }

        [Theory, InlineData("request")]
        public void ItReturnsTrue_WhenRequestFinished(string request)
        {
            var cache = new MoviesRequestsCache();

            cache.AddRequest(request);

            Assert.True(cache.FinishRequest(request));
        }

        [Theory, InlineData("request")]
        public void ItReturnsFalse_WhenRequestIsntFinished(string request)
        {
            var cache = new MoviesRequestsCache();

            cache.AddRequest(request);

            Assert.False(cache.IsRequestFinished(request));
        }

        [Theory, InlineData("request")]
        public void ItReturnsTrue_WhenRequestIsFinished(string request)
        {
            var cache = new MoviesRequestsCache();

            cache.AddRequest(request);

            cache.FinishRequest(request);

            Assert.True(cache.IsRequestFinished(request));
        }
    }
}
