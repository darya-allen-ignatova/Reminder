using DI.Reminder.Common.LoginModels;
using System.Collections.Generic;

namespace DI.Reminder.Data.RoleDatabase
{
    public interface IRoleRepository
    {
        List<Role> GetRoleList(int? id=null);
        bool DeleteRole(int id);
        void InsertRole(string Role);
        Role GetRoleByName(string Name);
    }
}
