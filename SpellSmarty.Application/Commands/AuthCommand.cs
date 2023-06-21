using MediatR;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public record AuthCommand : IRequest<BaseResponse<AuthResponseDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
