using AutoMapper;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.DataModels;

namespace SpellSmarty.Infrastructure.Mapper
{
    public class VideoMapper : Profile
    {
        public VideoMapper()
        {
            CreateMap<VideoModel, Video>();
            CreateMap<Video, VideoModel>()
                .ForMember(dest => dest.level, opt => opt.MapFrom(src => src.LevelNavigation.Name));
        }

    }
}
