using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.DataModels;

namespace SpellSmarty.Infrastructure.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly SpellSmartyContext _context;
        private readonly IMapper _mapper;

        public VideoRepository(SpellSmartyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VideoModel>> GetAll()
        {
            IEnumerable<Video> list = await _context.Videos
                .Include(vid=>vid.LevelNavigation)
                .Include(vid=>vid.VideoGenres)
                .ToListAsync();
            List<VideoModel> modelList = new List<VideoModel>();
            foreach (var video in list)
            {
                VideoModel videoModel = _mapper.Map<VideoModel>(video);

                List<string> genreNames = new List<string>();
                foreach (var videoGenre in video.VideoGenres)
                {
                    string genreName = _context.Genres
                        .FirstOrDefault(g => g.GenreId == videoGenre.GenreId)
                        ?.GenreName;

                    if (!string.IsNullOrEmpty(genreName))
                        genreNames.Add(genreName);
                }

                videoModel.VideoGenres = genreNames;
                modelList.Add(videoModel);
            }

            return modelList;
        }
    }
}
