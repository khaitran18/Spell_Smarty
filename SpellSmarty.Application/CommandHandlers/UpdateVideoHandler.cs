using AutoMapper;
using MediatR;
using SpellSmarty.Application.Commands;
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

    public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, VideoModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVideoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideoModel> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            VideoModel model = await _unitOfWork.VideosRepository.UpdateVideo(request.Videoid, request.Subtitle, request.VideoDescription, request.level, request.Premium);
            return model;
        }
    }
}
