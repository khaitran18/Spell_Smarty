using MediatR;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record GetVideosByGenreQuery(int genreId) : IRequest<IEnumerable<VideoDto>>;
}
