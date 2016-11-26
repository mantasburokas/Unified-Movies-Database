using Newtonsoft.Json;

namespace Movies.Models.Dtos
{
    public class MovieTmdb
    {
        [JsonProperty(PropertyName = "imdb_id")]
        public string ImdbId { get; set; }

        public Genre[] Genres { get; set; }
    }
}