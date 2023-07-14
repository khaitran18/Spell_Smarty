using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.CommandHandlers
{
    public class AddVideoHandler : IRequestHandler<AddVideoCommand, BaseResponse<VideoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddVideoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<VideoDto>> Handle(AddVideoCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<VideoDto> response = new BaseResponse<VideoDto>();
            try
            {
                VideoModel model = await _unitOfWork.VideosRepository.SaveVideo(request.rating, request.subtitle, request.thumbnaillink, request.channelname, request.srcid, request.title, request.learntcount, request.description, request.level, request.premium);
                response.Result = _mapper.Map<VideoDto>(model);
            }
            catch(Exception ex)
            {
                response.Error = true;
                response.Exception = ex;
                response.Message = "Error in the server";

            }
            return response;
        }
    }
}
