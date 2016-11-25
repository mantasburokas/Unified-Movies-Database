using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Movies.Clients.Interfaces;
using Movies.Models.Dtos;
using Newtonsoft.Json;

namespace Movies.Clients
{
    public class TmdbClient : ITmdbClient
    {
        private static int counter = 1;

        private const string GenresEndPoint = "genre/movie/list";

        private const string MoviesByGenreEndPoint = "genre/{genreId}/movies";

        private const string MovieByIdEndPoint = "movie/{movieId}";

        private const string ApiKeyFlag = "api_key=";

        private const string PageFlag = "page=";

        private const string ExternalIdsFlag = "append_to_response=external_ids";

        private readonly HttpClient _client;

        private readonly string _token;

        private readonly ISubject<MovieTmdb> _moviesTmdbSubject;

        public IObservable<MovieTmdb> MoviesTmdbObservable { get; }

        public TmdbClient(string baseUrl, string token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _token = token;

            _moviesTmdbSubject = new Subject<MovieTmdb>();

            MoviesTmdbObservable = _moviesTmdbSubject.AsObservable();
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

        public async Task GetMoviesByGenre(int genreId)
        {
            var page = 1;

            var totalPages = int.MinValue;

            do
            {
                var path = FormatPath(genreId, page);

                HttpResponseMessage response = null;

                try
                {
                    response = await _client.GetAsync(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var requestContent = await response.Content.ReadAsStringAsync();

                    var moviesDeserialized = JsonConvert.DeserializeObject<MoviesByGenreCollection>(requestContent);

                    if (totalPages == int.MinValue)
                    {
                        totalPages = moviesDeserialized.TotalPages;
                    }

                    foreach (var movieByGenre in moviesDeserialized.Movies)
                    {
                        var movieTmdb = await GetMovieById(movieByGenre.Id);

                        _moviesTmdbSubject.OnNext(movieTmdb);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{counter++} send movie with imdb id: {movieTmdb.ImdbId}");
                    }

                    page++;
                }

            } while (page <= totalPages);
        }

        protected async Task<MovieTmdb> GetMovieById(int id)
        {
            var path = FormatPath(id);

            MovieTmdb movie = null;

            while (movie == null)
            {
                var response = await _client.GetAsync(path);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var requestContent = await response.Content.ReadAsStringAsync();

                    movie = JsonConvert.DeserializeObject<MovieTmdb>(requestContent);
                }
                else
                {
                    Console.WriteLine("wtf");
                }

                if (movie == null)
                {
                    await Task.Delay(1000);
                }
            }

            return movie;
        }

        protected string FormatPath()
        {
            return GenresEndPoint + "?" + ApiKeyFlag + _token;
        }

        protected string FormatPath(int movieId)
        {
            return MovieByIdEndPoint.Replace("{" + nameof(movieId) + "}", movieId.ToString())
                   + "?" + ApiKeyFlag + _token + "&" + ExternalIdsFlag;
        }

        protected string FormatPath(int genreId, int page)
        {
            return MoviesByGenreEndPoint.Replace("{" + nameof(genreId) + "}", genreId.ToString())
                   + "?" + ApiKeyFlag + _token + "&" + PageFlag + page;
        }
    }
}