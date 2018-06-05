using DI.Reminder.Common.LoginModels;
using System.Collections.Generic;

namespace DI.Reminder.Data.AccountDatabase
{
    public interface IAccountRepository
    {
        void InsertAccount(Account account);
        Account GetAccount(string login);
        void DeleteAccount(int id);
        void UpdateAccount();
        List<Account> GetAccountList();
        Account GetAccount(int id);
    }
}
