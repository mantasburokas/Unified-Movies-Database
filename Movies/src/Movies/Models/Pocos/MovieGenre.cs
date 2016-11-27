using System.ComponentModel.DataAnnotations;

namespace Movies.Models.Pocos
{
    public class MovieGenre
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Released { get; set; }

        public Movie Movie { get; set; }

        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}