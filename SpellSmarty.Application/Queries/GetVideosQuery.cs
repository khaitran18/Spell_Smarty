using MediatR;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record GetVideosQuery() : IRequest<BaseResponse<IEnumerable<VideoDto>>>;
}
