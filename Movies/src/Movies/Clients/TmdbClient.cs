using Movies.Clients.Interfaces;
using Movies.Models.Dtos;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Movies.Clients
{
    public class TmdbClient : ITmdbClient
    {
        private const string GenresEndPoint = "genre/movie/list";

        private const string MoviesByGenreEndPoint = "genre/{genreId}/movies";

        private const string MovieByIdEndPoint = "movie/{movieId}";

        private const string MovieByImdbIdEndPoint = "find/{imdbId}";

        private const string ApiKeyFlag = "api_key=";

        private const string PageFlag = "page=";

        private const string ExternalIdsFlag = "append_to_response=external_ids";

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

            GenresCollection genres = null;

            while (genres == null)
            {
                var response = await _client.GetAsync(path);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var requestContent = await response.Content.ReadAsStringAsync();

                    genres = JsonConvert.DeserializeObject<GenresCollection>(requestContent);
                }

            }

            return genres;
        }

        public IObservable<MovieTmdb> GetMoviesByGenre(int genreId)
        {
            var movieSubject = new Subject<MovieTmdb>();

            GetMoviesByGenreInternal(genreId, movieSubject);

            return movieSubject.AsObservable();
        }

        public async Task<MoviesByImdbIdTmdb> GetMoviesByImdbId(string imdbId)
        {
            var path = FormatPath(imdbId);

            MoviesByImdbIdTmdb movies = null;

            while (movies == null)
            {
                var response = await _client.GetAsync(path);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var requestContent = await response.Content.ReadAsStringAsync();

                    movies = JsonConvert.DeserializeObject<MoviesByImdbIdTmdb>(requestContent);
                }
            }

            return movies;
        }

        protected async Task GetMoviesByGenreInternal(int genreId, ISubject<MovieTmdb> movieSubject)
        {
            var page = 1;

            var totalPages = int.MinValue;

            do
            {
                var path = FormatPath(genreId, page);

                var response = await _client.GetAsync(path);

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

                        if (!string.IsNullOrEmpty(movieTmdb?.ImdbId))
                        {
                            movieSubject.OnNext(movieTmdb);
                        }
                    }

                    page++;
                }

            } while (page <= totalPages);
        }

        protected async Task<MovieTmdb> GetMovieById(int id)
        {
            var path = FormatPath(id);

            MovieTmdb movie = null;

            HttpResponseMessage response;

            do
            {
                response = await _client.GetAsync(path);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var requestContent = await response.Content.ReadAsStringAsync();

                    movie = JsonConvert.DeserializeObject<MovieTmdb>(requestContent);
                }

                if (response.StatusCode == (HttpStatusCode)429)
                {
                    await Task.Delay(3000);
                }
            } while (movie == null && response.StatusCode != HttpStatusCode.NotFound);

            return movie;
        }

        protected string FormatPath()
        {
            return GenresEndPoint + "?" + ApiKeyFlag + _token;
        }

        protected string FormatPath(string imdbId)
        {
            return MovieByImdbIdEndPoint.Replace("{" + nameof(imdbId) + "}", imdbId) + "?" + ApiKeyFlag + _token;
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