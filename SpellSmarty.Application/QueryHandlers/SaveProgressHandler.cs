using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;

namespace SpellSmarty.Application.QueryHandlers
{
    public class SaveProgressHandler : IRequestHandler<SaveProgressQuery, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenService;

        public SaveProgressHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenServices tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<BaseResponse<string>> Handle(SaveProgressQuery request, CancellationToken cancellationToken)
        {
            BaseResponse<string> response = new BaseResponse<string>();
            try
            {
                string? id = _tokenService.ValidateToken(request.token)?.FindFirst("jti")?.Value;
                if (id!=null)
                {
                    int.TryParse(id,out int userId);
                    if (_unitOfWork.VideosRepository.ExistVideo(request.videoId).Result)
                    {
                        VideoStatModel model = await _unitOfWork.VideoStatRepository.SaveProgress(userId, request.videoId, request.progress);
                        VideoStatDto dto = _mapper.Map<VideoStatDto>(model);
                        response.Result = dto.progress;
                    }
                    else
                    {
                        response.Error = true;
                        response.Exception = new BadRequestException("Video doesnt exist");
                    }
                }
                else
                {
                    response.Error = true;
                    response.Exception = new BadRequestException("Invalid crefidential");
                }
            }
            catch (Exception e)
            {
                response.Error = true;
                response.Exception = e;
            }
            return response;   
        }
    }
}
