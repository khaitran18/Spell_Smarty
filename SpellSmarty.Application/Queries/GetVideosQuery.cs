using MediatR;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record GetVideosQuery() : IRequest<IEnumerable<VideoDto>>;
}
