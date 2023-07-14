using MediatR;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record GetSingleVideoQuery(int videoId, string? token) : IRequest<BaseResponse<VideoDto>>;
}
