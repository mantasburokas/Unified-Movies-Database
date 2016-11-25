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

        private const string ImdbIdFlag = "i=";

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
            var path = FormatPath(TitleFlag, title);

            var response = await _client.GetAsync(path);

            Movie movie = null;

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var requestContent = await response.Content.ReadAsStringAsync();

                movie = JsonConvert.DeserializeObject<Movie>(requestContent);
            }

            return movie;
        }

        public async Task<Movie> GetMovieByImdbId(string id)
        {
            var path = FormatPath(ImdbIdFlag, id);

            Movie movie = null;

            while (movie == null)
            {
                var response = await _client.GetAsync(path);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var requestContent = await response.Content.ReadAsStringAsync();

                    movie = JsonConvert.DeserializeObject<Movie>(requestContent);
                }

                if (movie == null)
                {
                    Console.WriteLine("Recieved a cooldown from Omdb");
                    await Task.Delay(300);
                }
            }

            return movie;
        }

        protected string FormatPath(string flag, string value)
        {
            return "?" + flag + value + "&" + RottenTomatoesFlag;
        }
    }
}