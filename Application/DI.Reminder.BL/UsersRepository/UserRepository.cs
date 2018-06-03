﻿using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void GetUser(string login)
        {
            if (login == null)
                return;
            _accountRepository.GetAccount(login);
        }

        public void GetUserList()
        {
            _accountRepository.GetAccountList();
        }

        public void InsertUser(Account account)
        {
            if (account == null)
                return;
            _accountRepository.InsertAccount(account);
        }
    }
}
