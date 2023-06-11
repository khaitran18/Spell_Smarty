using MediatR;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;

namespace SpellSmarty.Application.CommandHandlers
{
    public class VerifyAccountHandler : IRequestHandler<VerifyAccountCommand, Task>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenServices _tokenServices;

        public VerifyAccountHandler(IUnitOfWork unitOfWork, ITokenServices tokenServices)
        {
            _unitOfWork = unitOfWork;
            _tokenServices = tokenServices;
        }

        public async Task<Task> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            string? id = _tokenServices.ValidateToken(request.verifyToken)?.FindFirst("jti")?.Value;
            string? username = _tokenServices.ValidateToken(request.verifyToken)?.FindFirst("username")?.Value;
            if ((id != null) && (username != null))
            {
                var (userId,userName,role) = await _unitOfWork.AccountRepository.GetAccountDetailsByIdAsync(int.Parse(id));
                if (userName.Equals(username))
                {
                await _unitOfWork.AccountRepository.AddVerifyToken(userId);
                    return Task.CompletedTask;
                }
                else throw new BadRequestException("Invalid credential");
            }
            else throw new BadRequestException("Error in verifying email");
        }
    }
}
