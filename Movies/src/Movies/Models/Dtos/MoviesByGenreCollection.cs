using System.Collections.Generic;
using Newtonsoft.Json;

namespace Movies.Models.Dtos
{
    public class MoviesByGenreCollection
    {
        public int Page { get; set; }

        [JsonProperty(PropertyName = "results")]
        public ICollection<MovieByGenreTmdb> Movies { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }
    }
}