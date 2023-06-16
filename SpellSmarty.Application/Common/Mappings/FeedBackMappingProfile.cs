using AutoMapper;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Mappings
{
    public class FeedBackMappingProfile : Profile
    {
        public FeedBackMappingProfile()
        {
            CreateMap<FeedBackDto, FeedBackModel>();
            CreateMap<FeedBackModel, FeedBackDto>();
        }
    }
}
