using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Movies.Mappers;
using Movies.Models.Dtos;
using Xunit;
using GenrePoco = Movies.Models.Pocos.Genre;

namespace MapperTests
{

    #region ClassData

    public class ValidationDataGenreCollection : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>();

        public ValidationDataGenreCollection()
        {
            _data.Add(new object[]
            {
                new GenresCollection
                {
                    Genres = new[] {new Genre {Id = 1, Name = "Name"}}
                }
            });
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ValidationPocoCollection : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>();

        public ValidationPocoCollection()
        {
            _data.Add(new object[]
            {
                new List<GenrePoco>
                {
                    new GenrePoco
                    {
                        GenreId = 1,
                        Name = "Name"
                    }
                }
            });
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    #endregion

    public class GenreMapperTests
    {
        [Theory, ClassData(typeof(ValidationDataGenreCollection))]
        public void ItReturns_CorrectPocoCollection_FromGenreCollection(GenresCollection genreCollection)
        {
            var genreMapper = new GenresMapper();

            var pocoCollection = genreMapper.Map(genreCollection);

            for (var i = 0; i < genreCollection.Genres.Length; i++)
            {
                Assert.Equal(pocoCollection.ElementAt(i).GenreId, genreCollection.Genres[i].Id);
                Assert.Equal(pocoCollection.ElementAt(i).Name, genreCollection.Genres[i].Name);
            }
        }

        [Theory, ClassData(typeof(ValidationPocoCollection))]
        public void ItReturns_CorrectDtos_FromPocos(ICollection<GenrePoco> pocos)
        {
            var genreMapper = new GenresMapper();

            var genreDtos = genreMapper.Map(pocos);

            for (var i = 0; i < pocos.Count; i++)
            {
                Assert.Equal(genreDtos.ElementAt(i).Id, pocos.ElementAt(i).GenreId);
                Assert.Equal(genreDtos.ElementAt(i).Name, pocos.ElementAt(i).Name);
            }
        }
    }
}