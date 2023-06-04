using MediatR;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record GetSingleVideoQuery(int videoId) : IRequest<VideoDto>;
}
