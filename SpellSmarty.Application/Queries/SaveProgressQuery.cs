using MediatR;
using SpellSmarty.Application.Dtos;


namespace SpellSmarty.Application.Queries
{
    public record SaveProgressQuery(int videoId, string progress) : IRequest<VideoStatDto>;
}
