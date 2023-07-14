using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Response;
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
    public class GetVideosByUserIdHandler : IRequestHandler<GetVideosByUserIdQuery, BaseResponse<IEnumerable<VideoDto>>>
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

        public async Task<BaseResponse<IEnumerable<VideoDto>>> Handle(GetVideosByUserIdQuery request, CancellationToken cancellationToken)
        {
            BaseResponse<IEnumerable<VideoDto>> response = new BaseResponse<IEnumerable<VideoDto>>();
            try
            {
                string? id = _tokenService.ValidateToken(request.token)?.FindFirst("jti")?.Value;
                int userId = int.Parse(id);
                List<VideoDto> listDto = _mapper.Map<List<VideoDto>>(await _unitOfWork.VideosRepository.GetVideosByUserId(userId));
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
