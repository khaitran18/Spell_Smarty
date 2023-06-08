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

        public async Task<VideoStatModel> SaveProgress(int statid, string progress)
        {
            VideoStat videoStat = _context.VideoStats
                .Where(x => x.StatId == statid).FirstOrDefault();

            VideoStatModel videoStatModel = _mapper.Map<VideoStatModel>(videoStat);
            videoStatModel.Progress = progress;
            videoStat.Progress = progress;

             _context.Entry(videoStat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return videoStatModel;
        }
    }
}
