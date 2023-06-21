using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System.Security.Claims;

namespace SpellSmarty.Application.CommandHandlers
{
    public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand,BaseResponse<FeedBackDto>>
    {
        private readonly ITokenServices _tokenServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public CreateFeedbackHandler(ITokenServices tokenServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tokenServices = tokenServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<FeedBackDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<FeedBackDto> response = new BaseResponse<FeedBackDto>();
            try
            {
                if (request.token == null)
                {
                    response.Error = true;
                    response.Exception = new ForbiddenAccessException();
                    response.Message = "Token not found";
                }
                ClaimsPrincipal claim = _tokenServices.ValidateToken(request.token);
                int userId = int.Parse(claim.FindFirst("jti")?.Value);
                if (claim == null)
                {
                    response.Error = true;
                    response.Exception = new ForbiddenAccessException();
                    response.Message = "Invalid crefidential";
                }
                if (await _unitOfWork.VideosRepository.ExistVideo(request.videoId))
                {
                    FeedBackModel f = await _unitOfWork.FeedBackRepository.Create(userId, request.videoId, request.content);
                    response.Result = _mapper.Map<FeedBackDto>(f);
                }
                else
                {
                    response.Error = true;
                    response.Exception = new NotFoundException("Video not found");
                    response.Message = "Video not found";
                }
                return response;
            }
            catch (Exception e)
            {
                response.Error = true;
                response.Exception = e;
                response.Message = e.Message;
                return response;
            }
        }
    }
}
