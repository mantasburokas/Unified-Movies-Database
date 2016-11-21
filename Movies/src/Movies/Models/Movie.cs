using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class Movie
    {
        [Key]
        [Column(Order = 0)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Released { get; set; }

        [Required]
        public string ImdbRating { get; set; }

        [Required]
        public string Metascore { get; set; }

        [Required]
        public string TomatoMeter { get; set; }

        #region Relationships

        public ICollection<MovieGenre> MovieGenres { get; set; }

        #endregion
    }
}