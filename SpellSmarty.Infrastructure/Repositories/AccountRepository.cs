using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<AccountModel>, IAccountRepository
    {
        private readonly SpellSmartyContext _context;
        public AccountRepository(SpellSmartyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckAccount(int id)
        {
            return _context.Accounts.Any(a => a.Id == id);
        }
    }
}
