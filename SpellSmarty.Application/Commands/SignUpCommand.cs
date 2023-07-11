using MediatR;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public record SignUpCommand : IRequest<BaseResponse<AccountModel>>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set;} = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
    }
}
