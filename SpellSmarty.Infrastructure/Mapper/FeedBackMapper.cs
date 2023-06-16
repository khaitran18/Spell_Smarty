using AutoMapper;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Infrastructure.Mapper
{
    public class FeedBackMapper : Profile
    {
        public FeedBackMapper()
        {
            CreateMap<FeedBackModel, Feedback>();
            CreateMap<Feedback, FeedBackModel>();
        }
    }
}
