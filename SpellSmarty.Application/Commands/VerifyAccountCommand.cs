using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public record VerifyAccountCommand : IRequest<Task>
    {
        public string verifyToken { get; set; }
    }
}
