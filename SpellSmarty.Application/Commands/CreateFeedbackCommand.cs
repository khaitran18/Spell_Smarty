using MediatR;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Common.Response;

namespace SpellSmarty.Application.Commands
{
    public record CreateFeedbackCommand : IRequest<BaseResponse<FeedBackDto>>
    {
        public string? token { get; set; }
        public int videoId { get; set; }
        public string content { get; set; }
    }
}
