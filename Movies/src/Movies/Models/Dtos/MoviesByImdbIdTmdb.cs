using Newtonsoft.Json;

namespace Movies.Models.Dtos
{
    public class MoviesByImdbIdTmdb
    {
        [JsonProperty(PropertyName = "movie_results")]
        public MovieByGenreTmdb[] MoviesByGenre { get; set; }
    }
}