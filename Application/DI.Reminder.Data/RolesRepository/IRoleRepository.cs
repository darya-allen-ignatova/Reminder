using DI.Reminder.Common.LoginModels;
using System.Collections.Generic;

namespace DI.Reminder.Data.RolesRepository
{
    public interface IRoleRepository
    {
        List<Role> GetRoleList(int? id);
        void DeleteRole(int id);
        void InsertRole(string Role);
        IList<Role> GetAllRoles();
    }
}
