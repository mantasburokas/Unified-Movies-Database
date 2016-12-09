using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models.Pocos
{
    public class Movie
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(128)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Released { get; set; }

        [Required]
        [MaxLength(4)]
        public string ImdbRating { get; set; }

        [Required]
        [MaxLength(4)]
        public string Metascore { get; set; }

        [Required]
        [MaxLength(4)]
        public string TomatoMeter { get; set; }

        [Required]
        [MaxLength(128)]
        public string ImdbVotes { get; set; }

        [Required]
        [MaxLength(512)]
        public string Plot { get; set; }

        [Required]
        [MaxLength(32)]
        public string Runtime { get; set; }

        [Required]
        [MaxLength(64)]
        public string Director { get; set; }

        [Required]
        [MaxLength(128)]
        public string Awards { get; set; }

        #region Relationships

        public ICollection<MovieGenre> MovieGenres { get; set; }

        #endregion
    }
}