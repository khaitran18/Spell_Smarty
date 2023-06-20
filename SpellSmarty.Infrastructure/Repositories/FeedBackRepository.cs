using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class FeedBackRepository : BaseRepository<FeedBackModel>, IFeedBackRepository
    {
        private readonly SpellSmartyContext _context;
        private readonly IMapper _mapper;

        public FeedBackRepository(SpellSmartyContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FeedBackModel> Create(int userId, int videoId, string content)
        {
            Feedback feedback = new Feedback();
            feedback.Content = content;
            feedback.Date = DateTime.Now;
            feedback.AccountId = userId;
            feedback.Title = videoId.ToString();
            _context.Feedbacks.AddAsync(feedback);
            _context.SaveChanges();
            return _mapper.Map<FeedBackModel>(feedback) ;
        }

        public async Task<IEnumerable<FeedBackModel>> GetFeedBack()
        {
            IEnumerable<Feedback> list = await _context.Feedbacks
                .ToListAsync();
            List<FeedBackModel> modelList = _mapper.Map<List<FeedBackModel>>(list);
            return modelList;
        }
    }
}
