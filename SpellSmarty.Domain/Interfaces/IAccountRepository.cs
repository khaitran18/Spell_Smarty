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
    }
}
