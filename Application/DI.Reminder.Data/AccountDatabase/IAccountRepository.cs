using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data.AccountDatabase
{
    public interface IAccountRepository
    {
        void InsertAccount(Account account);
        Account GetAccount(string login, string password);
        void DeleteAccount(int id);
        void UpdateAccount();
        List<Account> GetAccountList();
    }
}
