using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.DataModels;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> GetProgressByUserIdAndVideoId(int userId, int videoId)
        {
            return await Task.FromResult(_context
                .VideoStats
                .FirstOrDefault(s => s.VideoId == videoId && s.AccountId == userId)
                .Progress);
        }

        public async Task<VideoStatModel> SaveProgress(int userId,int videoid, int progress)
        {
            VideoStatModel videoStatModel = new VideoStatModel();
            VideoStat videoStat = _context.VideoStats
                .Where(x => x.VideoId == videoid && x.AccountId == userId).FirstOrDefault();
            //if new videoStat
            if (videoStat == null)
            {
                VideoStat sta = new VideoStat
                {
                    AccountId = userId,
                    VideoId = videoid,
                    Progress = progress.ToString()
                };
                _context.VideoStats.Add(sta);
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
        private bool existProgress(string prg, int added)
        {
            if (prg.Contains(added.ToString())) return false;
            else return true;
        }
    }
}
