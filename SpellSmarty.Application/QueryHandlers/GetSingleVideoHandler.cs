using AutoMapper;
using MediatR;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Application.Services;
using System.Security.Claims;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Domain.Models;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetSingleVideoHandler : IRequestHandler<GetSingleVideoQuery, BaseResponse<VideoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenService;

        public GetSingleVideoHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenServices tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<BaseResponse<VideoDto>> Handle(GetSingleVideoQuery request, CancellationToken cancellationToken)
        {
            BaseResponse<VideoDto> response = new BaseResponse<VideoDto>();
            try
            {
                //check the user is in which role
                bool freeUser;
                ClaimsPrincipal claim = null;
                if (request.token == null)
                {
                    freeUser = true;
                }
                else
                {
                    claim = _tokenService.ValidateToken(request.token);
                    freeUser = (claim?.IsInRole("Free")) ?? false;
                }

                // Get video with Genre and Level
                VideoDto Dto = _mapper.Map<VideoDto>(await _unitOfWork.VideosRepository.GetVideoById(request.videoId));

                //Get progress if there is
                if (claim != null)
                {
                    int accountId = int.Parse(claim.FindFirst("jti").Value);
                    string? progress = await _unitOfWork.VideoStatRepository
                        .GetProgressByUserIdAndVideoId(accountId, request.videoId);
                    Dto.progress = progress;
                }

                // if free user, cant see premium subtitle
                if ((freeUser) && (Dto.Premium)) Dto.Subtitle = null;
                response.Result = Dto;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Exception = ex;
                response.Message = "Error in the server";
            }
            return response;
            
        }
    }
}
