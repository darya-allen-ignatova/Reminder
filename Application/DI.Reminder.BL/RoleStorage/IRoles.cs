using DI.Reminder.Common.LoginModels;
using System.Collections.Generic;

namespace DI.Reminder.BL.RoleStorage
{
    public interface IRoles
    {
        void InsertRole(Role role);
        void DeleteRole(int id);
        IList<Role> GetAllRoles();
        Role GetRole(int id);
        Role GetRoleByName(string Name);
    }
}
