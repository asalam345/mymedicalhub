using CRUD_DAL.Data;
using Domains;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DAL.Repository
{
    public class RepositoryUserRoleEnroll : IRepository<RoleEnrollment>
    {
        ApplicationDbContext _dbContext;
        public RepositoryUserRoleEnroll(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<RoleEnrollment> Create(RoleEnrollment _object)
        {
            var obj = await _dbContext.RoleEnrollments.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(RoleEnrollment _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<RoleEnrollment> GetAll()
        {
            try
            {
                return _dbContext.RoleEnrollments.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public RoleEnrollment GetById(int id)
        {
            return _dbContext.RoleEnrollments.Where(x => x.Id == id).FirstOrDefault();
        }
        //public string GetByUserId(int id)
        //{
        //    return _dbContext.RoleEnrollments.Where(x => x.UserId == id).Join(_dbContext.Roles, e => e.RoleId, r => r.Id, (e,r) => r.Title ).FirstOrDefault();
        //}
        public void Update(RoleEnrollment _object)
        {
            _dbContext.RoleEnrollments.Update(_object);
            _dbContext.SaveChanges();
        }
    }
}
