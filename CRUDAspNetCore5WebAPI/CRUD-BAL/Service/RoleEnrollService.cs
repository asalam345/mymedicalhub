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
    public class RoleEnrollService: IRoleEnrollService
	{
        private readonly IRepository<RoleEnrollment> _enroll;

        public RoleEnrollService(IRepository<RoleEnrollment> enroll)
        {
            _enroll = enroll;
        }
        //Get RoleEnrollment Details By RoleEnrollment Id
        public IEnumerable<RoleEnrollmentVM> GetRoleEnrollmentByRoleEnrollmentId(int userId)
        {
            List<RoleEnrollment> roleEnrollments = _enroll.GetAll().Where(x => x.Id == userId).ToList();
            List<RoleEnrollmentVM> roleEnrollmentVMs = RoleEnrollmentConverter.FromRoleEnrollment(roleEnrollments);
            return roleEnrollmentVMs;
        }
        //GET All Perso Details 
        public IEnumerable<RoleEnrollmentVM> GetAllRoleEnrollments()
        {
            try
            {
                //var roleEnrollmentVMs = _enroll.GetAll().ToList().ConvertAll(x => (RoleEnrollmentVM)x);
                List<RoleEnrollmentVM> roleEnrollmentVMs = new List<RoleEnrollmentVM>(_enroll.GetAll().ToList().Cast<RoleEnrollmentVM>());
                return roleEnrollmentVMs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RoleEnrollmentVM> AddRoleEnrollment(RoleEnrollmentVM roleEnrollmentVM)
        {
            var vmList = new List<RoleEnrollmentVM>() { roleEnrollmentVM };
            var roleEnrollment = RoleEnrollmentConverter.FromRoleEnrollmentVM(vmList).FirstOrDefault();

            var resultRoleEnrollment = await _enroll.Create(roleEnrollment);
            var list = new List<RoleEnrollment>() { resultRoleEnrollment };
            var vm = RoleEnrollmentConverter.FromRoleEnrollment(list).FirstOrDefault();

            return vm;
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
        public bool UpdateRoleEnrollment(RoleEnrollmentVM roleEnrollmentVM)
        {
            try
            {
                var vmList = new List<RoleEnrollmentVM>() { roleEnrollmentVM };
                var roleEnrollment = RoleEnrollmentConverter.FromRoleEnrollmentVM(vmList).FirstOrDefault();

                _enroll.Update(roleEnrollment);
                
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}