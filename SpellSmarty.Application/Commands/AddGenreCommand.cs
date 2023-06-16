using MediatR;
using SpellSmarty.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public record AddGenreCommand(string genrename) : IRequest<GenreDto>;
}
