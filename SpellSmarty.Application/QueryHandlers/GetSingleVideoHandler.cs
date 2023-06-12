using AutoMapper;
using MediatR;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Application.Services;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetSingleVideoHandler : IRequestHandler<GetSingleVideoQuery, VideoDto>
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

        public async Task<VideoDto> Handle(GetSingleVideoQuery request, CancellationToken cancellationToken)
        {
            bool freeUser;
            if (request.token == null) {
                freeUser = true;
            }
            else
            {
                freeUser = (_tokenService.ValidateToken(request.token)?.IsInRole("Free")) ?? false;
            }
            VideoDto Dto = _mapper.Map<VideoDto>(await _unitOfWork.VideosRepository.GetVideoById(request.videoId));
            if ((freeUser)&&(Dto.Premium)) Dto.Subtitle = null;
            return Dto;
        }
    }
}
