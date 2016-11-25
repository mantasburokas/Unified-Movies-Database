using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models.Dtos;

namespace Movies.Services.Interfaces
{
    public interface IGenresService
    {
        Task UpdateGenres();

        Task<ICollection<Genre>> GetGenres();
    }
}