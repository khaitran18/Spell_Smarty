using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Response;
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
    public class GetVideosByGenreHandler : IRequestHandler<GetVideosByGenreQuery, BaseResponse<IEnumerable<VideoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosByGenreHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<VideoDto>>> Handle(GetVideosByGenreQuery request, CancellationToken cancellationToken)
        {
            BaseResponse<IEnumerable<VideoDto>> response = new BaseResponse<IEnumerable<VideoDto>>();
            try
            {
                List<VideoDto> listDto = _mapper.Map<List<VideoDto>>(await _unitOfWork.VideosRepository.GetVideoByGenre(request.videoId));
                response.Result = listDto;
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
