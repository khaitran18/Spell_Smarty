using AutoMapper;
using MediatR;
using SpellSmarty.Application.Commands;
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
    public class SignUpHandler : IRequestHandler<SignUpCommand, AccountModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SignUpHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            AccountModel c = await _unitOfWork.AccountRepository.SignUpAsync(request.Username, request.Password, request.Email, request.Name);
            return c;
        }


        //public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
        //{
        //    bool c = await _unitOfWork.AccountRepository.SignUpAsync(request.Username,request.Password,request.Email,request.Name);
        //    return c;
        //}

    }
}
