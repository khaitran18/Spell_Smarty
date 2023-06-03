using AutoMapper;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Models;

namespace SpellSmarty.Application.Mappings
{
    public class VideoMappingProfile : Profile
    {
        public VideoMappingProfile()
        {
            CreateMap<VideoModel, VideoDto>();
            CreateMap<VideoDto, VideoModel>();
        }
    }
}
