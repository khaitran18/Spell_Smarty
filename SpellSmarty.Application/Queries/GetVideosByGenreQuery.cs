using MediatR;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record GetVideosByGenreQuery(int videoId) : IRequest<BaseResponse<IEnumerable<VideoDto>>>;
}
