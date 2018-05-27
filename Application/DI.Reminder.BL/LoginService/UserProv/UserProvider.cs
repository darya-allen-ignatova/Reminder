using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.BL.LoginService.UserIndent;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Data.RolesRepository;

namespace DI.Reminder.BL.LoginService.UserProv
{
    class UserProvider : IPrincipal
    {
        private IRoleRepository _roleRepository;
        private IAccountRepository _accountRepository;
        public UserProvider(IRoleRepository roleRepository, string name, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            userIndentity = new UserIndentity();
            userIndentity.Init(name,_accountRepository);
            _roleRepository = roleRepository;
        }
        private UserIndentity userIndentity { get; set; }
        public IIdentity Identity
        {
            get
            {
                return userIndentity;
            }
        }
        public bool IsInRole(string role)
        {
            if (userIndentity._account == null || string.IsNullOrWhiteSpace(role))
            {
                return false;
            }
            var UserRoles = _roleRepository.GetRoleList(userIndentity._account.ID);
            var rolesArray = role.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var _role in rolesArray)
            {
                var hasRole = UserRoles.Any(p => string.Compare(p.Name, role, true) == 0);
                if (hasRole)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
