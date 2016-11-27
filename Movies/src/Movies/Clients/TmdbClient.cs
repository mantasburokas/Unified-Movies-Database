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
using Movies.Clients.Helpers;

namespace Movies.Clients
{
    public class TmdbClient : ITmdbClient
    {
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

            movieSubject.OnCompleted();
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
            return TmdbClientHelper.GenresEndPoint + "?" + TmdbClientHelper.ApiKeyFlag + _token;
        }

        protected string FormatPath(string imdbId)
        {
            return TmdbClientHelper.MovieByImdbIdEndPoint.Replace("{" + nameof(imdbId) + "}", imdbId)
                   + "?" + TmdbClientHelper.ApiKeyFlag + _token + "&" + TmdbClientHelper.ExternalSourceFlag;
        }

        protected string FormatPath(int movieId)
        {
            return TmdbClientHelper.MovieByIdEndPoint.Replace("{" + nameof(movieId) + "}", movieId.ToString())
                   + "?" + TmdbClientHelper.ApiKeyFlag + _token + "&" + TmdbClientHelper.ExternalIdsFlag;
        }

        protected string FormatPath(int genreId, int page)
        {
            return TmdbClientHelper.MoviesByGenreEndPoint.Replace("{" + nameof(genreId) + "}", genreId.ToString())
                   + "?" + TmdbClientHelper.ApiKeyFlag + _token + "&" + TmdbClientHelper.PageFlag + page;
        }
    }
}