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
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile() 
        {
            CreateMap<GenreDto, GenreModel>();
            CreateMap<GenreModel, GenreDto>();
        }
    }
}
