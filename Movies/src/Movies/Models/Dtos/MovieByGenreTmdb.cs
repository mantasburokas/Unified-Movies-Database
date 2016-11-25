using System.Collections.Generic;
using Newtonsoft.Json;

namespace Movies.Models.Dtos
{
    public class MovieByGenreTmdb
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "genre_ids")]
        public ICollection<int> GenreIds { get; set; }

        public string Title { get; set; }

        public string Overview { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }
    }
}