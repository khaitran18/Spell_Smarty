using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
namespace SpellSmarty.Application.CommandHandlers
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, AccountModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly ITokenServices _tokenServices;

        public SignUpHandler(IUnitOfWork unitOfWork, IMailService mailService, ITokenServices tokenServices)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _tokenServices = tokenServices;
        }

        public async Task<AccountModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            AccountModel c = await _unitOfWork.AccountRepository.SignUpAsync(request.Username, request.Password, request.Email, request.Name);
            string verifyToken = _tokenServices.GenerateJWTToken((userId:c.Id, userName: c.Username, roles:c.Password));
            await _unitOfWork.AccountRepository.AddVerifyToken(c.Id,verifyToken);
            string verifyLink = "https://spellsmarty.vercel.app/verify/" + verifyToken;
            await _mailService.SendAsync(
                new MailDataModel
                {
                    To = new List<string> { request.Email},
                    Subject = "SpellSmarty - Verify your email",
                    Body = "Click in link to continue: " + verifyLink
                }
                , new CancellationToken());
            return c;
        }
    }
}
