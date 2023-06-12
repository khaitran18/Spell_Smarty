using MediatR;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record GetVideosByGenreQuery(int videoId) : IRequest<IEnumerable<VideoDto>>;
}
