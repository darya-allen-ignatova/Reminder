using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.UsersRepository
{
    public interface IUserRepository
    {
        void InsertUser(Account account);
        void DeleteUser(int? id);
        void GetUserList();
        void GetUser(string login);

    }
}
