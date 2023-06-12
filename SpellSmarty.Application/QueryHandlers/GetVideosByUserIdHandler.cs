using AutoMapper;
using MediatR;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetVideosByUserIdHandler : IRequestHandler<GetVideosByUserIdQuery, IEnumerable<VideoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenService;

        public GetVideosByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenServices tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<VideoDto>> Handle(GetVideosByUserIdQuery request, CancellationToken cancellationToken)
        {
            bool freeUser;
            if (request.token == null)
            {
                freeUser = true;
            }
            else
            {
                freeUser = (_tokenService.ValidateToken(request.token)?.IsInRole("Free")) ?? false;
            }
            List<VideoDto> listDto = _mapper.Map<List<VideoDto>>(await _unitOfWork.VideosRepository.GetVideosByUserId(request.userId));
            return listDto;
        }
    }
}
