using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;

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

        public async Task<IEnumerable<Video>> GetAll()
        {
            var list = await _context.Videos.ToListAsync();
            return _mapper.Map<IEnumerable<Video>>(list);
        }
    }
}
