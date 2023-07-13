using MediatR;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Application.Common.Response;

namespace SpellSmarty.Application.CommandHandlers
{
    public class VerifyAccountHandler : IRequestHandler<VerifyAccountCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenServices _tokenServices;

        public VerifyAccountHandler(IUnitOfWork unitOfWork, ITokenServices tokenServices)
        {
            _unitOfWork = unitOfWork;
            _tokenServices = tokenServices;
        }

        public async Task<BaseResponse<bool>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                if (request.verifyToken==null)
                {
                    response.Error = true;
                    response.Exception = new NotFoundException("Verification token not found");
                }
                string? id = _tokenServices.ValidateToken(request.verifyToken)?.FindFirst("jti")?.Value;
                bool c = false;
                if (id != null)
                {
                    c = await _unitOfWork.AccountRepository.CheckAccountVerificationToken(request.verifyToken, int.Parse(id));
                    if (c) await _unitOfWork.AccountRepository.VerifyAccount(int.Parse(id));
                    else throw new BadRequestException("Invalid credential");
                }
                else throw new BadRequestException("Error in verifying email");
                response.Result = true;
            }
            catch (Exception e)
            {
                response.Error = true;
                response.Exception = e;
            }
            return response;
        }
    }
}
