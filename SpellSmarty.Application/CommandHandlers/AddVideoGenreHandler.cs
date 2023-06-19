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
    public class AddVideoGenreHandler : IRequestHandler<AddVideoGenreCommand, VideoGenreModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddVideoGenreHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideoGenreModel> Handle(AddVideoGenreCommand request, CancellationToken cancellationToken)
        {
            VideoGenreModel model = await _unitOfWork.VideoGenreRepository.AddVideoGenre(request.VideoId, request.GenreId);
            return model;
        }
    }
}
