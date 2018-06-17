using DI.Reminder.BL.Cache;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Data.RoleDatabase;
using System;
using System.Collections.Generic;

namespace DI.Reminder.BL.UsersService
{
    public class UserService : IUserService
    {
        private ICacheService _cacheService;
        private IAccountRepository _accountRepository;
        private IRoleRepository _roleRepository;
        public UserService(IAccountRepository accountRepository, ICacheService cacheService, IRoleRepository roleRepository)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }
        public void DeleteUser(int id)
        {
            if (id < 1)
                return;
            _accountRepository.DeleteAccount(id);
            _cacheService.DeleteCache(id);
        }

        public void EditUser(Account account)
        {
            _accountRepository.UpdateAccount(account);
            account.Roles = _roleRepository.GetRoleList(account.ID);
            _cacheService.UpdateCache(account, account.ID);
        }

        public Account GetUser(string login)
        {
            return _accountRepository.GetAccount(login);
        }

        public Account GetUser(int id)
        {
            if (id < 1)
                return null;
            var account = _cacheService.GetValueOfCache<Account>(id);
            if(account==null)
            {
                account = _accountRepository.GetAccount(id);
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
            _accountRepository.InsertAccount(account);
            _cacheService.AddCache<Account>(account, account.ID);
        }
    }
}
