using DI.Reminder.BL.CachedRepository;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using System;
using System.Collections.Generic;

namespace DI.Reminder.BL.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private ICacheRepository _cacheRepository;
        private IAccountRepository _accountRepository;
        public UserRepository(IAccountRepository accountRepository, ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }
        public void DeleteUser(int? id)
        {
            if (id == null || id < 1)
                return;
            _accountRepository.DeleteAccount((int)id);
            _cacheRepository.DeleteCache((int)id);
        }

        public void EditUser(Account account)
        {
            if (account == null)
                return;
            _accountRepository.UpdateAccount(account);
            _cacheRepository.UpdateCache(account, account.ID);
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
            var account = _cacheRepository.GetValueOfCache<Account>((int)id);
            if(account==null)
            {
                account = _accountRepository.GetAccount((int)id);
                _cacheRepository.AddCache<Account>(account, account.ID);
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
            _cacheRepository.AddCache<Account>(account, account.ID);
        }
    }
}
