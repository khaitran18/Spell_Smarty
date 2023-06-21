using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.DataModels;

namespace SpellSmarty.Infrastructure.Repositories
{
    public class VideoStatRepository : BaseRepository<VideoStatModel>, IVideoStatRepository
    {
        private readonly SpellSmartyContext _context;
        private readonly IMapper _mapper;

        public VideoStatRepository(SpellSmartyContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string?> GetProgressByUserIdAndVideoId(int userId, int videoId)
        {
            string progress = null;
            var stat = _context
                .VideoStats
                .FirstOrDefault(s => s.VideoId == videoId && s.AccountId == userId);
            if (stat != null)
            {
                progress = stat.Progress;
            }
            return await Task.FromResult(progress);
        }

        public async Task<VideoStatModel> SaveProgress(int userId,int videoid, string progress)
        {
            VideoStatModel videoStatModel = new VideoStatModel();
            VideoStat? videoStat = _context.VideoStats
                .FirstOrDefault(x => x.VideoId == videoid && x.AccountId == userId);
            //if new videoStat
            if (videoStat == null)
            {
                videoStat = new VideoStat
                {
                    AccountId = userId,
                    VideoId = videoid,
                    Progress = progress
                };
                _context.VideoStats.Add(videoStat);
                videoStatModel = _mapper.Map<VideoStatModel>(videoStat);
            }
            else 
            {
            videoStatModel = _mapper.Map<VideoStatModel>(videoStat);
                if (existProgress(videoStat.Progress, progress))
                {
                    string newProgress = videoStat.Progress + " " + progress;
                    videoStatModel.Progress = newProgress;
                    videoStat.Progress = newProgress;
                    _context.Entry(videoStat).State = EntityState.Modified;
                }
                else throw new BadRequestException("Progress existed");
            }
            await _context.SaveChangesAsync();
            return videoStatModel;
        }
        private bool existProgress(string prg, string added)
        {
            if (prg.Contains(added.ToString())) return false;
            else return true;
        }
    }
}
