using AutoMapper;
using MediatR;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;

namespace SpellSmarty.Application.QueryHandlers
{
    public class SaveProgressHandler : IRequestHandler<SaveProgressQuery, string>
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

        public async Task<string> Handle(SaveProgressQuery request, CancellationToken cancellationToken)
        {
            if (isNumber(request.progress))
            {
                string? id = _tokenService.ValidateToken(request.token)?.FindFirst("jti")?.Value;
                int userId = int.Parse(id);
                VideoStatModel model = await _unitOfWork.VideoStatRepository.SaveProgress(userId, request.videoId, request.progress);
                VideoStatDto dto = _mapper.Map<VideoStatDto>(model);
                return dto.progress;
            }
            else
            {
                throw new BadRequestException("Wrong progress type");
            }
        }

        private bool isNumber(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //public async Task<VideoStatDto> Handle(SaveProgressQuery request, CancellationToken cancellationToken)
        //{
        //    string? id = _tokenService.ValidateToken(request.token)?.FindFirst("jti")?.Value;
        //    int userId = int.Parse(id);
        //    VideoStatDto listDto = _mapper.Map<VideoStatDto>(await _unitOfWork.VideoStatRepository.SaveProgress(userId,request.videoId ,request.progress));
        //    return listDto;
        //}
    }
}
