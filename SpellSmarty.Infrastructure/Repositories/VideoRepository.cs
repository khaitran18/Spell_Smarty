using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.DataModels;

namespace SpellSmarty.Infrastructure.Repositories
{
    public class VideoRepository : BaseRepository<VideoModel>, IVideoRepository
    {
        private readonly SpellSmartyContext _context;
        private readonly IMapper _mapper;

        public VideoRepository(SpellSmartyContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        // using seperate method than base method to include genre into video
        public async Task<IEnumerable<VideoModel>> GetAllWithGenre()
        {
            IEnumerable<Video> list = await _context.Videos
                .Include(vid=>vid.LevelNavigation)
                .Include(vid=>vid.VideoGenres)
                .ToListAsync();

            // map genre name into VideoModel, I dont use mapper cause it require the context to get Genre
            List<VideoModel> modelList = new List<VideoModel>();
            foreach (var video in list)
            {
                // mapping video(List<int> videoGenre) to a model(List<string> videoGenre)
                VideoModel videoModel = _mapper.Map<VideoModel>(video);
                //generate List<string> genreNames
                var genreNames = video.VideoGenres.Join(_context.Genres
                    , vd => vd.GenreId
                    , genre => genre.GenreId
                    , (name, genre) => genre.GenreName
                    );
                //map VideoModel with Video
                videoModel.VideoGenres = genreNames;
                modelList.Add(videoModel);
            }

            return modelList;
        }
    }
}
