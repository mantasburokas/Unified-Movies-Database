using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Movies.Clients.Interfaces;
using Movies.Models.Dtos;
using Newtonsoft.Json;

namespace Movies.Clients
{
    public class TmdbClient : ITmdbClient
    {
        private const string GenresEndPoint = "genre/movie/list";

        private const string MoviesByGenreEndPoint = "genre/{genreId}/movies";

        private const string ApiKeyFlag = "api_key=";

        private const string PageFlag = "page=";

        private readonly HttpClient _client;

        private readonly string _token;

        public TmdbClient(string baseUrl, string token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _token = token;
        }

        public async Task<GenresCollection> GetGenres()
        {
            var path = FormatPath();

            var response = await _client.GetAsync(path);

            GenresCollection genres = null;

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var requestContent = await response.Content.ReadAsStringAsync();

                genres = JsonConvert.DeserializeObject<GenresCollection>(requestContent);
            }

            return genres;
        }

        public async Task<MoviesByGenreCollection> GetMoviesByGenre(int genreId)
        {
            MoviesByGenreCollection moviesCollection = new MoviesByGenreCollection
            {
                Page = 1
            };

            do
            {
                var path = FormatPath(genreId, moviesCollection.Page);

                var response = await _client.GetAsync(path);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var requestContent = await response.Content.ReadAsStringAsync();

                    var moviesDeserialized = JsonConvert.DeserializeObject<MoviesByGenreCollection>(requestContent);

                    if (moviesCollection.Movies == null)
                    {
                        moviesCollection.Movies = moviesDeserialized.Movies;
                        moviesCollection.TotalPages = moviesDeserialized.TotalPages;
                    }
                    else
                    {
                        moviesCollection.Movies = moviesCollection.Movies.Concat(moviesDeserialized.Movies).ToList();
                    }

                    moviesCollection.Page++;
                }

                Console.WriteLine(moviesCollection.Page);
            } while (moviesCollection.Page <= moviesCollection.TotalPages); 

            return moviesCollection;
        }

        protected string FormatPath()
        {
            return GenresEndPoint + "?" + ApiKeyFlag + _token;
        }

        protected string FormatPath(int genreId, int page)
        {
            return MoviesByGenreEndPoint.Replace("{" + nameof(genreId) + "}", genreId.ToString())
                   + "?" + ApiKeyFlag + _token + "&" + PageFlag + page;
        }
    }
}