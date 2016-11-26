namespace Movies.Models.Dtos
{
    public class MovieOmdb
    {
        public string ImdbId { get; set; }

        public string Title { get; set; }

        public string Released { get; set; }

        public string ImdbRating { get; set; }

        public string Metascore { get; set; }

        public string TomatoMeter { get; set; }
    }
}