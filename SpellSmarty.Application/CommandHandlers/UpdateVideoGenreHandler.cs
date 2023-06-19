using AutoMapper;
using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.CommandHandlers
{
    public class UpdateVideoGenreHandler : IRequestHandler<UpdateVideoGenreCommand, VideoGenreModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVideoGenreHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideoGenreModel> Handle(UpdateVideoGenreCommand request, CancellationToken cancellationToken)
        {
            VideoGenreModel model = await _unitOfWork.VideoGenreRepository.UpdateVideoGenre(request.VideoId);
            return model;
        }
    }
}
