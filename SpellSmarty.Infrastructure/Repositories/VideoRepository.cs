using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.DataModels;
using System.Collections.Generic;

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

        public async Task<IEnumerable<VideoModel>> GetVideoByGenre(int videoId)
        {
            var a = _context.VideoGenres.Where(x => x.VideoId == videoId).Select(x => x.GenreId).ToList();

            if(!a.Any())
            {
                return Enumerable.Empty<VideoModel>();  
            }

            List<Video> list = new List<Video>();
            foreach (var genre in a)
            {
               IEnumerable<Video> videolist = await _context.Videos
              .Include(vid => vid.VideoGenres)
              .Where(x => x.VideoGenres.FirstOrDefault().GenreId == genre && x.Videoid != videoId)
              .ToListAsync();
                
              list.AddRange(videolist);
            }
               
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

        public async Task<VideoModel> GetVideoById(int videoid)
        {
            try
            {
                Video? video = _context.Videos
                .Include(x => x.VideoGenres)
                .Include(x => x.LevelNavigation)
                .FirstOrDefault(x => x.Videoid == videoid);
                if (video != null)
                {
                    VideoModel videoModel = _mapper.Map<VideoModel>(video);
                    var genreNames = video.VideoGenres.Join(_context.Genres
                            , vd => vd.GenreId
                            , genre => genre.GenreId
                            , (name, genre) => genre.GenreName
                            );
                    videoModel.VideoGenres = genreNames;
                    return videoModel;
                }
                else throw new NotFoundException("Video not found");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<VideoModel>> GetVideosByCreator(int videoId)
        {
            var creator = _context.Videos
                .Include(vid => vid.LevelNavigation)
                .Include(vid => vid.VideoGenres)
                .Where(x => x.Videoid == videoId)
                .Select(x => x.ChannelName).First();

            IEnumerable<Video> videolist = await _context.Videos
             .Include(vid => vid.VideoGenres)
             .Where(x => x.ChannelName == creator && x.Videoid != videoId)
             .ToListAsync();

            List<VideoModel> modelList = new List<VideoModel>();
            foreach (var video in videolist)
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

        public async Task<IEnumerable<VideoModel>> GetVideosByUserId(int userId)
        {
            IEnumerable<Video> list = await _context.Videos
                .Include(vid => vid.LevelNavigation)
                .Include(vid => vid.VideoGenres)
                .Include(vid => vid.VideoStats)
                .Where(x => x.VideoStats.FirstOrDefault().AccountId == userId)
                .ToListAsync();
            List<VideoModel> modelList = new List<VideoModel>();
            foreach (var video in list)
            {
                VideoModel videoModel = _mapper.Map<VideoModel>(video);
                var genreNames = video.VideoGenres.Join(_context.Genres
                    , vd => vd.GenreId
                    , genre => genre.GenreId
                    , (name, genre) => genre.GenreName
                    );
                var progess = video.VideoStats.FirstOrDefault().Progress;
                videoModel.progress = progess;
                videoModel.VideoGenres = genreNames;
                modelList.Add(videoModel);
            }
            return modelList;
        }
    }
}
