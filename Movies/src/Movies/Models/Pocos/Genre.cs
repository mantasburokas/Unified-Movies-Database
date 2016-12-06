using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models.Pocos
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        #region Relationships

        public ICollection<MovieGenre> MovieGenres { get; set; }

        #endregion
    }
}