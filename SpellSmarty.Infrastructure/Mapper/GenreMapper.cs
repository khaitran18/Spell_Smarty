using AutoMapper;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Infrastructure.Mapper
{
    public class GenreMapper : Profile
    {
        public GenreMapper()
        {
            CreateMap<GenreModel, Genre>();
            CreateMap<Genre, GenreModel>();
        }
    }
}
