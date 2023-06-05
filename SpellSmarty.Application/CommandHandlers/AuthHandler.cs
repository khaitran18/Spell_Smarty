using MediatR;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.CommandHandlers
{
    public class AuthHandler : IRequestHandler<AuthCommand, AuthResponseDto>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthHandler(ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork)
        {
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            int userId = await _unitOfWork.AccountRepository.CheckAccountAsync(request.UserName, request.Password);
            if (userId==-1)
            {
                throw new BadRequestException("Invalid username or password");
            }
            else
            {
                var (id, username, plan) = await _unitOfWork.AccountRepository.GetAccountDetailsByIdAsync(userId);
                string token = _tokenGenerator.GenerateJWTToken((userId: id, userName: username, plan: plan));
                return new AuthResponseDto()
                {
                    UserId = userId,
                    Name = username,
                    Role = plan,
                    Token = token
                };
            }
        }
    }
}
