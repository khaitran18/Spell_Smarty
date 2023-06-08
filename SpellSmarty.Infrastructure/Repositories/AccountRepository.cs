using AutoMapper;
using Ordering.Application.Common.Exceptions;
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
                {
                    if (acc.EmailVerify == true)
                    {
                        return await Task.FromResult(acc.Id);
                    }
                    else return await Task.FromResult(0);
                }
            }
            return await Task.FromResult(-1);
        }

        public async Task<(int userId, string UserName, string plan)> GetAccountDetailsByIdAsync(int id)
        {
            Account account = _context.Accounts.FirstOrDefault(a => a.Id == id);
            AccountModel acc = _mapper.Map <AccountModel>(account);
            return await Task.FromResult(
                (acc.Id
                ,acc.Username
                ,_context.Plans.FirstOrDefault(p=>p.Planid== acc.Planid).PlanName)
                );
        }

        public async Task<bool> ExistUsername(string username) => _context.Accounts.Any(a => a.Username.Equals(username));
        public async Task<bool> ExistEmail(string email) => _context.Accounts.Any(a => a.Email.Equals(email));

        public async Task<AccountModel> SignUpAsync(string username, string password, string email, string name)
        {
            if (await ExistEmail(email))
            {
                throw new BadRequestException("Email existed");
            }
            if (await ExistUsername(username))
            {
                throw new BadRequestException("Username existed");
            }

            // SEND EMAIL SERVICE HERE
            ///////////////////////////
            ///
            /// 
            /// 
            /// 
            /// 
            ///////////////////////////
            ///
            AccountModel am = new AccountModel
            {
                Email = email,
                Name = name,
                Password = password,
                Username = username,
                EmailVerify = false
            };
            if (_context.Accounts.AddAsync(_mapper.Map<Account>(am)).IsCompletedSuccessfully)
            {
                _context.SaveChanges();
                return am;
            }
            else throw new BadRequestException("Error in creating new account");
        }
    }
}
