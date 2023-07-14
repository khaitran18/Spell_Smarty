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
using SpellSmarty.Application.Common.Response;

namespace SpellSmarty.Application.CommandHandlers
{
    public class AddGenreHandler : IRequestHandler<AddGenreCommand, BaseResponse<GenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddGenreHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<GenreDto>> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<GenreDto> response = new BaseResponse<GenreDto>();
            try
            {
                GenreModel model = await _unitOfWork.GenreRepository.AddGenre(request.genrename);
                response.Result = _mapper.Map<GenreDto>(model);
            }catch(Exception ex)
            {
                response.Error = true;
                response.Exception = ex;
                response.Message = "Error in the server";
            }
            return response;
        }
    }
}
