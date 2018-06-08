using System.Collections.Generic;
using System.Linq;
using DI.Reminder.BL.CachedRepository;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.RolesRepository;

namespace DI.Reminder.BL.RoleStorage
{
    public class Roles : IRoles
    {
        ICacheRepository _cacheRepository;
        IRoleRepository _roleRepository;
        public Roles(IRoleRepository roleRepository, ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
            _roleRepository = roleRepository;
        }
        public void DeleteRole(int? id)
        {
            if (id < 0 || id == null)
                return;
            _roleRepository.DeleteRole((int)id);
            _cacheRepository.DeleteCache((int)id);
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
            _cacheRepository.AddCache(role, role.ID);
        }
        public Role GetRole(int? id)
        {
            if (id < 0 || id == null)
                return null;
            var role = _cacheRepository.GetValueOfCache<Role>((int)id);
            if(role==null)
            {
                IList<Role> list = _roleRepository.GetAllRoles();
                role=list.FirstOrDefault(t => t.ID == id);
                _cacheRepository.AddCache(role, role.ID);
            }
            return role;
        }
    }
}
