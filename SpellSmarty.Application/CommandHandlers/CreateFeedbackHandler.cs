using AutoMapper;
using MediatR;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System.Security.Claims;

namespace SpellSmarty.Application.CommandHandlers
{
    public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand, FeedBackDto>
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

        public async Task<FeedBackDto> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            FeedBackDto response = new FeedBackDto();
            try
            {
                if (request.token == null)
                {
                    response.Error = true;
                    response.Exception = new ForbiddenAccessException();
                }
                ClaimsPrincipal claim = _tokenServices.ValidateToken(request.token);
                int userId = int.Parse(claim.FindFirst("jti")?.Value);
                if (claim == null)
                {
                    response.Error = true;
                    response.Exception = new ForbiddenAccessException();
                    return response;
                }
                FeedBackModel f = await _unitOfWork.FeedBackRepository.Create(userId,request.videoId,request.content);
                response = _mapper.Map<FeedBackDto>(f);
                return response;
            }
            catch (Exception e) 
            {
                response.Error = true;
                response.Exception = e;
                return response;
            }
        }
    }
}
