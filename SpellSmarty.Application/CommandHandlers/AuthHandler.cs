using MediatR;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;

namespace SpellSmarty.Application.CommandHandlers
{
    public class AuthHandler : IRequestHandler<AuthCommand, BaseResponse<AuthResponseDto>>
    {
        private readonly ITokenServices _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthHandler(ITokenServices tokenGenerator, IUnitOfWork unitOfWork)
        {
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<AuthResponseDto>> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<AuthResponseDto> authResponse = new BaseResponse<AuthResponseDto>();
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
                    authResponse.Result = new AuthResponseDto()
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
                authResponse.Exception = e;
                return authResponse;
            }   
        }
    }
}
