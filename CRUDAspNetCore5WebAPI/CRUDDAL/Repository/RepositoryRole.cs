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
    public class RepositoryRole : IRepository<Role>
    {
        ApplicationDbContext _dbContext;
        public RepositoryRole(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Role> Create(Role _object)
        {
            var obj = await _dbContext.Roles.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Role _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Role> GetAll()
        {
            try
            {
                return _dbContext.Roles.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Role GetById(int id)
        {
            return _dbContext.Roles.Where(x => x.Id == id).FirstOrDefault();
            //return _dbContext.RoleEnrollments.Where(x => x.UserId == id).Join(_dbContext.Roles, e => e.RoleId, r => r.Id, (e, r) => new Role { r.Title, r.Id}).FirstOrDefault();
        }
    
        public void Update(Role _object)
        {
            _dbContext.Roles.Update(_object);
            _dbContext.SaveChanges();
        }
    }
}
