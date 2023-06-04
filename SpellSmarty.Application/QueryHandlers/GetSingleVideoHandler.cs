using AutoMapper;
using MediatR;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetSingleVideoHandler : IRequestHandler<GetSingleVideoQuery, VideoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSingleVideoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideoDto> Handle(GetSingleVideoQuery request, CancellationToken cancellationToken)
        {
            VideoDto Dto = _mapper.Map<VideoDto>(await _unitOfWork.VideosRepository.GetVideoById(request.videoId));
            return Dto;
        }
    }
}
