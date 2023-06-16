using MediatR;
using SpellSmarty.Application.Commands;
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
    public class UpgradePremiumHandler : IRequestHandler<UpgradePremiumCommand, AccountModel?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        public UpgradePremiumHandler(IUnitOfWork unitOfWork, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        public async Task<AccountModel?> Handle(UpgradePremiumCommand request, CancellationToken cancellationToken)
        {
            var check = await _unitOfWork.AccountRepository.UpgradePremiumUser(request.AccountId, request.Months);
            if (check != null)
            {
                await _mailService.SendAsync(
                new MailDataModel
                {
                    To = new List<string> { check.Email },
                    Subject = "SpellSmarty - Inform Subscription",
                    Body = $@"
                              <h2 style=""color: #0c7cd5; font-family: Arial, sans-serif; font-size: 24px; margin-bottom: 20px;"">Spell Smarty</h2>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">Dear valued user,</p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">Thank you for being a part of Spell Smarty. We appreciate your support!</p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">We are pleased to inform you that your account has been upgraded to Premium. You will enjoy the premium features and benefits for {request.Months} months from <strong>{check.SubribeDate?.ToString("dd-MM-yyyy")}</strong> to <strong>{check.EndDate?.ToString("dd-MM-yyyy")}</strong>.</p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">If you have any questions or need further assistance, please don't hesitate to reach out to our support team.</p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">Thank you again for choosing Spell Smarty!</p>
  
                              <hr style=""border: none; border-top: 1px solid #ccc; margin: 20px 0;"">
  
                              <p style=""color: #888; font-family: Arial, sans-serif; font-size: 14px;"">Best regards,<br>The Spell Smarty Team</p>"
            }
                , new CancellationToken());
        }
            return check;
        }
}
}
