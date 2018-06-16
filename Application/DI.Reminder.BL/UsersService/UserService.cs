using DI.Reminder.BL.Cache;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using System;
using System.Collections.Generic;

namespace DI.Reminder.BL.UsersService
{
    public class UserService : IUserService
    {
        private ICacheService _cacheService;
        private IAccountRepository _accountRepository;
        public UserService(IAccountRepository accountRepository, ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }
        public void DeleteUser(int? id)
        {
            if (id == null || id < 1)
                return;
            _accountRepository.DeleteAccount((int)id);
            _cacheService.DeleteCache((int)id);
        }

        public void EditUser(Account account)
        {
            if (account == null)
                return;
            _accountRepository.UpdateAccount(account);
            _cacheService.UpdateCache(account, account.ID);
        }

        public Account GetUser(string login)
        {
            if (login == null)
                return null;
            return _accountRepository.GetAccount(login);
        }

        public Account GetUser(int? id)
        {
            if (id == null || id < 1)
                return null;
            var account = _cacheService.GetValueOfCache<Account>((int)id);
            if(account==null)
            {
                account = _accountRepository.GetAccount((int)id);
                _cacheService.AddCache<Account>(account, account.ID);
            }
            return account;
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
            _cacheService.AddCache<Account>(account, account.ID);
        }
    }
}
