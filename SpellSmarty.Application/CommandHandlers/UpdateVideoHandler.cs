using AutoMapper;
using MediatR;
using SpellSmarty.Application.Commands;
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

    public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, BaseResponse<VideoModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVideoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<VideoModel>> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<VideoModel> response = new BaseResponse<VideoModel>();
            try
            {
                VideoModel model = await _unitOfWork.VideosRepository.UpdateVideo(request.Videoid, request.Subtitle, request.SrcId, request.Title, request.VideoDescription, request.level, request.Premium);
                response.Result = model;
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
