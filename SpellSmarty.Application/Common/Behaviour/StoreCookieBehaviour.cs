using MediatR;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Behaviour
{
    public class StoreCookieBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : AuthCommand
    {
        private readonly ICookieService _cookieService;

        public StoreCookieBehaviour(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();
            if (response is AuthResponseDto authResponseDto)
            {
                _cookieService.WriteCookie("token", authResponseDto.Token, 1);
            }
            return response;
        }
    }
}
