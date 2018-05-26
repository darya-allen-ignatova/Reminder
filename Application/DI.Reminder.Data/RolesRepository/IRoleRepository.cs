using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data.RolesRepository
{
    public interface IRoleRepository
    {
        List<Role> GetRoleList(int? id);
        void UpdateRole();
        void DeleteRole(int id);
        void InsertRole(string Role);
    }
}
