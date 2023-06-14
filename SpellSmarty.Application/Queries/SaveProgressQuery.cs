using MediatR;
using SpellSmarty.Application.Dtos;


namespace SpellSmarty.Application.Queries
{
    public record SaveProgressQuery(string? token,int videoId, int progress) : IRequest<string>;
}
