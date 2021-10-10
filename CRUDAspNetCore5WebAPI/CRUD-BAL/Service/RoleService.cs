using CRUD_DAL.Interface;
using CRUD_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_BAL.Service
{
    public class RoleService
    {
        private readonly IRepository<Role> _role;

        public RoleService(IRepository<Role> role)
        {
            _role = role;
        }
        public IEnumerable<Role> GetRoleById(int roleId)
        {
            return _role.GetAll().Where(x => x.Id == roleId).ToList();
        }
        //GET All Perso Details 
        public IEnumerable<Role> GetAllRoles()
        {
            try
            {
                return _role.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<Role> AddRole(Role role)
        {
            return await _role.Create(role);
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
        //Update Role Details
        public bool UpdateRole(Role role)
        {
            try
            {
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