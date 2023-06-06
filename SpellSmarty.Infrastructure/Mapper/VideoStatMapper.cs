using AutoMapper;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Infrastructure.Mapper
{
    public class VideoStatMapper : Profile
    {
        public VideoStatMapper() 
        {
            CreateMap<VideoStatModel, VideoStat>();
            CreateMap<VideoStat, VideoStatModel>();
        }
    }
}
