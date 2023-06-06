using MediatR;
using SpellSmarty.Application.Dtos;


namespace SpellSmarty.Application.Queries
{
    public record SaveProgressQuery(int statId, int progress) : IRequest<VideoStatDto>;
}
