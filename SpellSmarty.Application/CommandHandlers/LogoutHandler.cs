using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.CommandHandlers
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, BaseResponse<bool>>
    {
        private readonly ICookieService _cookieService;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutHandler(ICookieService cookieService, IUnitOfWork unitOfWork)
        {
            _cookieService = cookieService;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                bool r= _cookieService.DeleteCookie("token");
                if (!r)
                {
                    response.Error = true;
                    response.Exception = new BadRequestException("Error");
                }
                response.Result = r;
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
