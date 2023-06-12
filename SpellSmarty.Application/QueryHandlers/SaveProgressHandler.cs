using AutoMapper;
using MediatR;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;


namespace SpellSmarty.Application.QueryHandlers
{
    public class SaveProgressHandler : IRequestHandler<SaveProgressQuery, VideoStatDto>
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

        public async Task<VideoStatDto> Handle(SaveProgressQuery request, CancellationToken cancellationToken)
        {
            string? id = _tokenService.ValidateToken(request.token)?.FindFirst("jti")?.Value;
            int userId = int.Parse(id);
            VideoStatDto listDto = _mapper.Map<VideoStatDto>(await _unitOfWork.VideoStatRepository.SaveProgress(userId,request.videoId ,request.progress));
            return listDto;
        }
    }
}
