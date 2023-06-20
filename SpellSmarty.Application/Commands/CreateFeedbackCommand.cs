using MediatR;
using SpellSmarty.Application.Common.Dtos;

namespace SpellSmarty.Application.Commands
{
    public record CreateFeedbackCommand : IRequest<FeedBackDto>
    {
        public string? token { get; set; }
        public int videoId { get; set; }
        public string content { get; set; }
    }
}
