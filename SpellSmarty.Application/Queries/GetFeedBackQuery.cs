using MediatR;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Queries
{
    public record GetFeedBackQuery : IRequest<IEnumerable<FeedBackDto>>;
}
