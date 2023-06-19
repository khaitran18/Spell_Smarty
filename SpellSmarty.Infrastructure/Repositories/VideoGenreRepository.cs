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
    public class VideoGenreRepository : BaseRepository<VideoGenreModel>, IVideoGenreRepository
    {
        private readonly SpellSmartyContext _context;
        private readonly IMapper _mapper;

        public VideoGenreRepository(SpellSmartyContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VideoGenreModel> AddVideoGenre(int videoId, int genreId)
        {
            VideoGenre videoGenre = new VideoGenre
            {
                VideoId = videoId,
                GenreId = genreId,
            };
            _context.VideoGenres.Add(videoGenre);
            VideoGenreModel model = _mapper.Map<VideoGenreModel>(videoGenre);
            await _context.SaveChangesAsync();
            return model;
        }

        public Task<VideoGenreModel> UpdateVideoGenre(int videoId)
        {
            throw new NotImplementedException();
        }
    }
}
