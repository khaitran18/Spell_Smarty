using MediatR;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;

namespace SpellSmarty.Application.CommandHandlers
{
    public class AuthHandler : IRequestHandler<AuthCommand, AuthResponseDto>
    {
        private readonly ITokenServices _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthHandler(ITokenServices tokenGenerator, IUnitOfWork unitOfWork)
        {
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            AuthResponseDto authResponse = new AuthResponseDto();
            try
            {
                int userId = await _unitOfWork.AccountRepository.CheckAccountAsync(request.UserName, request.Password);
                if (userId == -1)
                {
                    authResponse.Error = true;
                    authResponse.Exception = new BadRequestException("Wrong username or password");
                }
                else if (userId == 0)
                {
                    authResponse.Error = true;
                    authResponse.Exception = new BadRequestException("Email not verify");
                }
                else
                {
                    var (id, username, plan) = await _unitOfWork.AccountRepository.GetAccountDetailsByIdAsync(userId);
                    string token = _tokenGenerator.GenerateJWTToken((userId: id, userName: username, plan: plan));
                    authResponse = new AuthResponseDto()
                    {
                        UserId = userId,
                        Name = username,
                        Role = plan,
                        Token = token
                    };
                }
                return authResponse;
            }
            catch (Exception e)
            {
                authResponse.Error = true;
                authResponse.Message = e.Message;
                return authResponse;
            }
            
        }
    }
}
