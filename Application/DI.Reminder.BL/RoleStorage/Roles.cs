using System;
using System.Collections.Generic;
using System.Linq;
using DI.Reminder.BL.Cache;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.RoleDatabase;

namespace DI.Reminder.BL.RoleStorage
{
    public class Roles : IRoles
    {
        private ICacheService _cacheService;
        private IRoleRepository _roleRepository;
        public Roles(IRoleRepository roleRepository, ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }
        public void DeleteRole(int id)
        {
            if (id < 1)
                return;
            _roleRepository.DeleteRole(id);
            _cacheService.DeleteCache(id);
        }

        public IList<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public void InsertRole(Role role)
        {
            if (role.Name == null)
                return;
            _roleRepository.InsertRole(role.Name);
            _cacheService.AddCache(role, role.ID);
        }
        public Role GetRole(int id)
        {
            if (id < 1)
                return null;
            var role = _cacheService.GetValueOfCache<Role>(id);
            if(role==null)
            {
                IList<Role> list = _roleRepository.GetAllRoles();
                role=list.FirstOrDefault(t => t.ID == id);
                _cacheService.AddCache(role, role.ID);
            }
            return role;
        }

        public Role GetRoleByName(string Name)
        {
            return _roleRepository.GetRoleByName(Name);
        }
    }
}
