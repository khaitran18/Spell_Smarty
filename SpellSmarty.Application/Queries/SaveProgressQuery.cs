using MediatR;
using SpellSmarty.Application.Dtos;


namespace SpellSmarty.Application.Queries
{
    public record SaveProgressQuery(string? token,int videoId, string progress) : IRequest<VideoStatDto>;
}
