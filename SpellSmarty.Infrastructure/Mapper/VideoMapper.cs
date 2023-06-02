using AutoMapper;

namespace SpellSmarty.Infrastructure.Mapper
{
    public class VideoMapper : Profile
    {
        public VideoMapper()
        {
            CreateMap<SpellSmarty.Infrastructure.DataModels.Video, SpellSmarty.Domain.Models.Video>();
            CreateMap<SpellSmarty.Domain.Models.Video, SpellSmarty.Infrastructure.DataModels.Video>();
        }
    }
}
