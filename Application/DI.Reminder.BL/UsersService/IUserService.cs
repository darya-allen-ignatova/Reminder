using DI.Reminder.Common.LoginModels;
using System.Collections.Generic;

namespace DI.Reminder.BL.UsersService
{
    public interface IUserService
    {
        void InsertUser(Account account);
        void EditUser(Account account);
        void DeleteUser(int id);
        IList<Account> GetUserList();
        Account GetUser(string login);
        Account GetUser(int id);

    }
}
