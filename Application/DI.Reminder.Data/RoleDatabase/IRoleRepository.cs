using DI.Reminder.Common.LoginModels;
using System.Collections.Generic;

namespace DI.Reminder.Data.RoleDatabase
{
    public interface IRoleRepository
    {
        Role GetRole(int? id);
        bool DeleteRole(int id);
        void InsertRole(string Role);
        Role GetRoleByName(string Name);
        List<Role> GetRoleList();
    }
}
