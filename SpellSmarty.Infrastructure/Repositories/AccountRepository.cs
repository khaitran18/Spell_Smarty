using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Exceptions;
using SpellSmarty.Application.Common.Dtos;
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
        public AccountRepository(SpellSmartyContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CheckAccountAsync(string username, string password)
        {
            var acc = _context.Accounts.FirstOrDefault(a => a.Username.Equals(username));
            if (acc != null)
            {
                if (acc.Password.Equals(password))
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
            AccountModel acc = _mapper.Map<AccountModel>(account);
            return await Task.FromResult(
                (acc.Id
                , acc.Username
                , _context.Plans.FirstOrDefault(p => p.Planid == acc.Planid).PlanName)
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

            AccountModel am = new AccountModel
            {
                Email = email,
                Name = name,
                Password = password,
                Username = username,
                EmailVerify = false
            };
            Account acc = _mapper.Map<Account>(am);
            if (_context.Accounts.AddAsync(acc).IsCompletedSuccessfully)
            {
                _context.SaveChanges();
                am.Id = acc.Id;
                return am;
            }
            else throw new BadRequestException("Error in creating new account");
        }

        public async Task<bool> AddVerifyToken(int id, string verifyToken)
        {
            Account? a = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (a != null)
            {
                a.VerifyToken = verifyToken;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> VerifyAccount(int userId)
        {
            Account? a = _context.Accounts.FirstOrDefault(a => a.Id == userId);
            if (a != null)
            {
                a.EmailVerify = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CheckAccountVerificationToken(string verificationToken, int? id)
        {
            if (id == null)
            {
                return await Task.FromResult(false);
            }
            Account? a = _context.Accounts
                .FirstOrDefault(a => a.Id == id);
            if (a != null)
            {
                if (verificationToken.Equals(a.VerifyToken.Trim()))
                    return await Task.FromResult(true);
                else
                    return await Task.FromResult(false);
            }
            else
                return await Task.FromResult(false);
        }

        public async Task<IEnumerable<AccountModel>?> GetAllAccountsAsync()
        {
            var accountList = new List<AccountModel>();
            try
            {
                accountList = _mapper.Map(await _context.Accounts.Include(a => a.Plan).ToListAsync(), accountList);
            }
            catch (Exception)
            {

                throw;
            }
            return accountList;
        }

        public async Task<AccountModel?> UpgradePremiumUser(int userId, int months)
        {
            var check = new AccountModel();
            try
            {
                var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == userId);
                if (user != null)
                {
                    if (user.Planid != 2)
                    {
                        user.Planid = 2;
                        user.SubribeDate = DateTime.Now;
                        user.EndDate = DateTime.Now.AddMonths(months);
                        _context.Accounts.Update(user);
                        await _context.SaveChangesAsync();
                        check = _mapper.Map(user, check);
                    }
                }
            }
            catch (Exception)
            {
                check = null;
                throw;
            }
            return check;
        }
    }
}
