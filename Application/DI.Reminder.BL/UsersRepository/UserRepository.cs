using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using System;
using System.Collections.Generic;

namespace DI.Reminder.BL.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        IAccountRepository _accountRepository;
        public UserRepository(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public void DeleteUser(int? id)
        {
            if (id == null || id < 0)
                return;
            _accountRepository.DeleteAccount((int)id);
        }

        public void EditUser(Account account)
        {
            throw new NotImplementedException();
        }

        public Account GetUser(string login)
        {
            if (login == null)
                return null;
            return _accountRepository.GetAccount(login);
        }

        public Account GetUser(int? id)
        {
            if (id == null || id < 0)
                return null;
            return(_accountRepository.GetAccount((int)id));
        }

        public IList<Account> GetUserList()
        {
            return _accountRepository.GetAccountList();
        }

        public void InsertUser(Account account)
        {
            if (account == null)
                return;
            _accountRepository.InsertAccount(account);
        }
    }
}
