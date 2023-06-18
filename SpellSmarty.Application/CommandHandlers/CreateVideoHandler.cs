using AutoMapper;
using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Dtos;
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
    public class CreateVideoHandler : IRequestHandler<CreateVideoCommand, VideoModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateVideoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideoModel> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            VideoModel model = await _unitOfWork.VideosRepository.SaveVideo(request.rating, request.subtitle, request.thumbnaillink, request.channelname, request.srcid, request.title, request.learntcount, request.description, request.level, request.premium);
            return model;
        }
    }
}
