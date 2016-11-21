namespace Movies.Models
{
    public class MovieGenre
    {
        public string Title { get; set; }

        public string Released { get; set; }

        public Movie Movie { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}