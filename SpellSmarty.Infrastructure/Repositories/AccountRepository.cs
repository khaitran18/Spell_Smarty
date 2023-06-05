using AutoMapper;
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
        private readonly IMapper _mapper;
        public AccountRepository(SpellSmartyContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CheckAccountAsync(string username, string password)
        {
            var acc = _context.Accounts.FirstOrDefault(a => a.Username.Equals(username));
            if (acc != null)
            {
                if (acc.Password.Equals(_context.Accounts.FirstOrDefault(a => a.Username.Equals(username)).Password))
                    return await Task.FromResult(acc.Id);
            }
            return await Task.FromResult(-1);
        }

        public async Task<(int userId, string UserName, string plan)> GetAccountDetailsByIdAsync(int id)
        {
            Account account = _context.Accounts.FirstOrDefault(a => a.Id == id);
            AccountModel acc = _mapper.Map < AccountModel > (account);
            return await Task.FromResult(
                (acc.Id
                ,acc.Username
                ,_context.Plans.FirstOrDefault(p=>p.Planid== acc.Planid).PlanName)
                );
        }
    }
}
