using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IGenreRepository : IBaseRepository<GenreModel>
    {
        Task<GenreModel> AddGenre(string genreName);
    }
}
