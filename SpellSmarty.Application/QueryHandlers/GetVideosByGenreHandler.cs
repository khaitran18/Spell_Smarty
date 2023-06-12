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
    public class GetVideosByGenreHandler : IRequestHandler<GetVideosByGenreQuery, IEnumerable<VideoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosByGenreHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VideoDto>> Handle(GetVideosByGenreQuery request, CancellationToken cancellationToken)
        {
            List<VideoDto> listDto = _mapper.Map<List<VideoDto>>(await _unitOfWork.VideosRepository.GetVideoByGenre(request.genreId));
            return listDto;
        }
    }
}
