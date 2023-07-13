using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetVideosHandler : IRequestHandler<GetVideosQuery, BaseResponse<IEnumerable<VideoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<VideoDto>>> Handle(GetVideosQuery request, CancellationToken cancellationToken)
        {
            BaseResponse<IEnumerable<VideoDto>> response = new BaseResponse<IEnumerable<VideoDto>>();
            try
            {
                List<VideoDto> listDto = _mapper.Map<List<VideoDto>>(await _unitOfWork.VideosRepository.GetAllWithGenre());
                response.Result = listDto;
            }
            catch (Exception)
            {
                response.Error = true;
                response.Exception = new Exception("Error in the server");
            }
            return response;
        }
    }
}
