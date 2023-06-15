using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.CommandHandlers
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly ICookieService _cookieService;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutHandler(ICookieService cookieService, IUnitOfWork unitOfWork)
        {
            _cookieService = cookieService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_cookieService.DeleteCookie("token"));
        }
    }
}
