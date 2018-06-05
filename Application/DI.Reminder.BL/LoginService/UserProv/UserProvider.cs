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
    public class UserProvider : IPrincipal
    {
        private IRoleRepository _roleRepository;
        private IAccountRepository _accountRepository;
        public UserProvider(IRoleRepository roleRepository, IAccountRepository accountRepository, string name )
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;


            userIndentity = new UserIndentity(_accountRepository);
            userIndentity.Init(name);
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
            if (userIndentity.account == null || string.IsNullOrWhiteSpace(role))
            {
                return false;
            }
            var UserRoles = _roleRepository.GetRoleList(userIndentity.account.ID);
            foreach (var _role in UserRoles)
            {
                var hasRole = UserRoles.Any(p => string.Compare(_role.Name.Replace(" ", string.Empty), role, true) == 0);
                if (hasRole)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
