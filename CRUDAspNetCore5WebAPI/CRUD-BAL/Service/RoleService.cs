using CRUD_BAL.Converter;
using Domains;
using Interfaces.Repository;
using Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace CRUD_BAL.Service
{
    public class RoleService: IRoleService
    {
        private readonly IRepository<Role> _role;

        public RoleService(IRepository<Role> role)
        {
            _role = role;
        }
        public IEnumerable<RoleVM> GetRoleById(int roleId)
        {
            var roles = _role.GetAll().Where(x => x.Id == roleId).ToList();
            var roleVMs = RoleConverter.FromRole(roles);
            return roleVMs;
        }
        //GET All Perso Details 
        public IEnumerable<RoleVM> GetAllRoles()
        {
            try
            {
                var roles = _role.GetAll().ToList();
                var roleVMs = RoleConverter.FromRole(roles);
                return roleVMs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<RoleVM> AddRole(RoleVM roleVM)
        {
            var vmList = new List<RoleVM>() { roleVM };
            var role = RoleConverter.FromRoleVM(vmList).FirstOrDefault();

            var resultRole = await _role.Create(role);
            var resultRoleList = new List<Role>() { resultRole };
            var resultVM = RoleConverter.FromRole(resultRoleList).FirstOrDefault();

            return resultVM;
        }
        //Delete Role 
        public bool DeleteRole(int id)
        {
            try
            {
                var DataList = _role.GetAll().Where(x => x.Id == id).ToList();
                foreach (var item in DataList)
                {
                    _role.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        public bool UpdateRole(RoleVM roleVM)
        {
            try
            {
                var vmList = new List<RoleVM>() { roleVM };
                var role = RoleConverter.FromRoleVM(vmList).FirstOrDefault();

                _role.Update(role);
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}