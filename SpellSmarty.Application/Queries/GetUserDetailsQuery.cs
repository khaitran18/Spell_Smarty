using MediatR;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Queries
{
    public record GetUserDetailsQuery(string token) : IRequest<AccountModel?>
    {
    }
}
