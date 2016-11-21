using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Movies.Clients.Interfaces;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.Clients
{
    public class OmdbClient : IOmdbClient
    {
        private const string TitleFlag = "t=";

        private const string RottenTomatoesFlag = "tomatoes=true";

        private readonly HttpClient _client;

        public OmdbClient(string baseUrl)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            var path = FormatPath(title);

            var response = await _client.GetAsync(path);

            Movie movie = null;

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var requestContent = await response.Content.ReadAsStringAsync();

                movie = JsonConvert.DeserializeObject<Movie>(requestContent);
            }

            return movie;
        }

        protected string FormatPath(string title)
        {
            return "?" + TitleFlag + title + "&" + RottenTomatoesFlag;
        }
    }
}