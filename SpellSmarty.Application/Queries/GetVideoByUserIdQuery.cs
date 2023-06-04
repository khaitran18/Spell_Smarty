using MediatR;
using SpellSmarty.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Queries
{
    public record GetVideosByUserIdQuery(int userId) : IRequest<IEnumerable<VideoDto>>;
}
