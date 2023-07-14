using AutoMapper;
using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.CommandHandlers
{
    public class UpdateVideoGenreHandler : IRequestHandler<UpdateVideoGenreCommand, BaseResponse<VideoGenreModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVideoGenreHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<VideoGenreModel>> Handle(UpdateVideoGenreCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<VideoGenreModel> response = new BaseResponse<VideoGenreModel>();
            try
            {
                VideoGenreModel model = await _unitOfWork.VideoGenreRepository.UpdateVideoGenre(request.VideoId);
                response.Result = model;
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
