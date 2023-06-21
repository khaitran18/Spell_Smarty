using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.CommandHandlers
{
    public class AddGenreHandler : IRequestHandler<AddGenreCommand, GenreDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddGenreHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenreDto> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            GenreModel model = await _unitOfWork.GenreRepository.AddGenre(request.genrename);
            GenreDto dto = _mapper.Map<GenreDto>(model);
            return dto;
        }
    }
}
