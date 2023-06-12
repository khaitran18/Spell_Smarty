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
            bool c = false;
            if (id != null){
                c = await _unitOfWork.AccountRepository.CheckAccountVerificationToken(request.verifyToken,int.Parse(id));
                if (c) await _unitOfWork.AccountRepository.VerifyAccount(int.Parse(id));
                else throw new BadRequestException("Invalid credential");
            }
            else throw new BadRequestException("Error in verifying email");
            return Task.CompletedTask;
        }
    }
}
