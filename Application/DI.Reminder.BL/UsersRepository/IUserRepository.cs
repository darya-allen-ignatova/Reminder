using DI.Reminder.Common.LoginModels;
using System.Collections.Generic;

namespace DI.Reminder.BL.UsersRepository
{
    public interface IUserRepository
    {
        void InsertUser(Account account);
        void EditUser(Account account);
        void DeleteUser(int? id);
        IList<Account> GetUserList();
        Account GetUser(string login);
        Account GetUser(int? id);

    }
}
