using AutoMapper;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<GenreModel>, IGenreRepository
    {
        private readonly SpellSmartyContext _context;
        private readonly IMapper _mapper;

        public GenreRepository(SpellSmartyContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenreModel> AddGenre(string genreName)
        {

            Genre genre = new Genre
            {
                GenreName = genreName,
            };
            _context.Genres.Add(genre);
            GenreModel genreModel = _mapper.Map<GenreModel>(genre);
            await _context.SaveChangesAsync();
            return genreModel;
        }
    }
}
