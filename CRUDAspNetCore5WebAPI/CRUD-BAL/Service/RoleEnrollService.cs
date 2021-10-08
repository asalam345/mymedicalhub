using CRUD_DAL.Interface;
using CRUD_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_BAL.Service
{
    public class RoleEnrollService
    {
        private readonly IRepository<RoleEnrollment> _enroll;

        public RoleEnrollService(IRepository<RoleEnrollment> enroll)
        {
            _enroll = enroll;
        }
        //Get RoleEnrollment Details By RoleEnrollment Id
        public IEnumerable<RoleEnrollment> GetRoleEnrollmentByRoleEnrollmentId(int userId)
        {
            return _enroll.GetAll().Where(x => x.Id == userId).ToList();
        }
        //GET All Perso Details 
        public IEnumerable<RoleEnrollment> GetAllRoleEnrollments()
        {
            try
            {
                return _enroll.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get RoleEnrollment by RoleEnrollment Name
        //public string GetRoleByUserId(int userId)
        //{
        //    return _enroll.
        //}
        //Add RoleEnrollment
        public async Task<RoleEnrollment> AddRoleEnrollment(RoleEnrollment RoleEnrollment)
        {
            return await _enroll.Create(RoleEnrollment);
        }
        //Delete RoleEnrollment 
        public bool DeleteRoleEnrollment(int id)
        {

            try
            {
                var DataList = _enroll.GetAll().Where(x => x.Id == id).ToList();
                foreach (var item in DataList)
                {
                    _enroll.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        //Update RoleEnrollment Details
        public bool UpdateRoleEnrollment(RoleEnrollment user)
        {
            try
            {
                var DataList = _enroll.GetAll().ToList();
                foreach (var item in DataList)
                {
                    _enroll.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}