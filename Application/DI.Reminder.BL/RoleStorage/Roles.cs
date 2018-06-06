using System.Collections.Generic;
using System.Linq;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.RolesRepository;

namespace DI.Reminder.BL.RoleStorage
{
    public class Roles : IRoles
    {
        IRoleRepository _roleRepository;
        public Roles(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public void DeleteRole(int? id)
        {
            if (id < 0 || id == null)
                return;
            _roleRepository.DeleteRole((int)id);
        }

        public IList<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public void InsertRole(Role role)
        {
            if (role == null || role.Name == null)
                return;
            _roleRepository.InsertRole(role.Name);
        }
        public Role GetRole(int? id)
        {
            if (id < 0 || id == null)
                return null;
            IList<Role> list = _roleRepository.GetAllRoles();
            return list.FirstOrDefault(t => t.ID == id);
        }
    }
}
