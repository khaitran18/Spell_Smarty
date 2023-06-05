using MediatR;
using SpellSmarty.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public class AuthCommand : IRequest<AuthResponseDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
