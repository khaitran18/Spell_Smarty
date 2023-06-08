using AutoMapper;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Mappings
{
    public class VideoStatMappingProfile : Profile
    {
        public VideoStatMappingProfile()
        {
            CreateMap<VideoStatDto, VideoStatModel>();
            CreateMap<VideoStatModel, VideoStatDto>();
        }
    }
}
