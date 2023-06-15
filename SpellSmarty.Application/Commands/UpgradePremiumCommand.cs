using MediatR;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public class UpgradePremiumCommand : IRequest<AccountModel>
    {
        public int AccountId { get; set; }
        public int Months { get; set; } = 0;
    }
}
