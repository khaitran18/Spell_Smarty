using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IAccountRepository:IBaseRepository<AccountModel>
    {
        Task<int> CheckAccountAsync(string username, string password);
        Task<(int userId, string UserName, string plan)> GetAccountDetailsByIdAsync(int id);
        Task<AccountModel> SignUpAsync(string username, string password, string email,string name);
        Task<bool> AddVerifyToken(int id, string verifyToken);
        Task<bool> VerifyAccount(int userId);
        public Task<bool> CheckAccountVerificationToken(string verificationToken, int? id);
        public Task<IEnumerable<AccountModel>?> GetAllAccountsAsync();
        public Task<AccountModel?> UpgradePremiumUser(int userId, int months);
    }
}
