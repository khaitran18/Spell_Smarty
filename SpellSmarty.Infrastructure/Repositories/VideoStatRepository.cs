using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<VideoStatModel> SaveProgress(int userId,int videoid, string progress)
        {
            VideoStatModel videoStatModel1 = new VideoStatModel();
            VideoStat videoStat = _context.VideoStats
                .Where(x => x.VideoId == videoid && x.AccountId == userId).FirstOrDefault();
            if (videoStat == null)
            {
                VideoStat sta = new VideoStat
                {
                    AccountId = userId,
                    VideoId = videoid,
                    Progress = progress,
                };
                _context.VideoStats.Add(sta);
            }
            else 
            { 
            VideoStatModel videoStatModel = _mapper.Map<VideoStatModel>(videoStat);
            string newProgress = videoStat.Progress +" "+ progress;
            videoStatModel.Progress = newProgress;
            videoStat.Progress = newProgress;

             _context.Entry(videoStat).State = EntityState.Modified;
            }
                await _context.SaveChangesAsync();
            return videoStatModel1;
        }
    }
}
