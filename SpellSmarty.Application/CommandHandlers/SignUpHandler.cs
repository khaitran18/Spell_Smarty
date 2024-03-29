﻿using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
namespace SpellSmarty.Application.CommandHandlers
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, BaseResponse<AccountModel>>
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

        public async Task<BaseResponse<AccountModel>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<AccountModel> response = new BaseResponse<AccountModel>();
            try
            {
                if (_unitOfWork.AccountRepository.ExistUsername(request.Username).Result)
                {
                    response.Error = true;
                    response.Exception = new BadRequestException("Username existed");
                }
                else if (_unitOfWork.AccountRepository.ExistEmail(request.Email).Result)
                {
                    response.Error = true;
                    response.Exception = new BadRequestException("Email existed");
                }
                else
                {
                    AccountModel c = await _unitOfWork.AccountRepository.SignUpAsync(request.Username, request.Password, request.Email, request.Name);
                    if (c == null)
                    {
                        response.Error = true;
                        response.Exception = new Exception("Error in creating account");
                    }
                    string verifyToken = _tokenServices.GenerateJWTToken((userId: c.Id, userName: c.Username, roles: c.Password));

                    await _unitOfWork.AccountRepository.AddVerifyToken(c.Id, verifyToken);

                    string verifyLink = "https://spellsmarty.com/verify/" + verifyToken;
                    await _mailService.SendAsync(
                        new MailDataModel
                        {
                            To = new List<string> { request.Email },
                            Subject = "SpellSmarty - Verify your email",
                            Body = $@"
                              <h2 style=""color: #0c7cd5; font-family: Arial, sans-serif; font-size: 24px; margin-bottom: 20px;"">Spell Smarty</h2>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">{request.Name},</p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">Thank you for your registration at Spell Smarty. We appreciate your support!</p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;""> Click the following link to verify your email before proceeding: <a href='{verifyLink}'>Here</a></p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">If you have any questions or need further assistance, please don't hesitate to reach out to our support team.</p>
  
                              <p style=""color: #555; font-family: Arial, sans-serif; font-size: 16px; margin-bottom: 10px;"">Thank you again for choosing Spell Smarty!</p>
  
                              <hr style=""border: none; border-top: 1px solid #ccc; margin: 20px 0;"">
  
                              <p style=""color: #888; font-family: Arial, sans-serif; font-size: 14px;"">Best regards,<br>The Spell Smarty Team</p>"
                        }
                        , new CancellationToken());
                    response.Result = c;
                }
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
