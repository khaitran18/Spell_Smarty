using MediatR;
using SpellSmarty.Application.Dtos;

namespace SpellSmarty.Application.Queries
{
    public record AddVideoCommand(float? rating,
                                string subtitle,
                                string? thumbnaillink,
                                string? channelname,
                                string srcid,
                                string title,
                                int learntcount,
                                string description,
                                int level,
                                bool premium) : IRequest<VideoDto>;
}
