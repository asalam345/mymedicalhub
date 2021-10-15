using DAL.Data;
using Domains;
using Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RepositoryUser : IRepository<User>, IUser
    {
        ApplicationDbContext _dbContext;
        public RepositoryUser(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<User> Create(User _object)
        {
            var obj = await _dbContext.Users.AddAsync(_object);
            _dbContext.SaveChanges();
            if (obj.Entity.Id > 0)
			{
                return obj.Entity;
            }
            return null;           
        }

        public void Delete(User _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _dbContext.Users.Where(x => x.IsDeleted == false).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User GetById(int Id)
        {
            return _dbContext.Users.Where(x => x.IsDeleted == false && x.Id == Id).AsNoTracking().FirstOrDefault();
        }

        public void Update(User _object)
        {
            _dbContext.Users.Update(_object);
            _dbContext.SaveChanges();
        }
       
		public User IsExists(User user)
		{
            return _dbContext.Users.Where(x => x.Email == user.Email || x.Mobile == user.Mobile).AsNoTracking().FirstOrDefault();
        }

		public bool IsCodeExists(RegConfirmation regCon)
		{
            var row = _dbContext.RegConfirmations.Where(w => w.UserId == regCon.UserId && w.Device == regCon.Device && w.Code == regCon.Code).AsNoTracking().FirstOrDefault();
            if (row == null) return false;
            return true;
        }

		public async Task<RegConfirmation> Create(RegConfirmation regCon)
		{
			try
			{
                var obj = await _dbContext.RegConfirmations.AddAsync(regCon);
                _dbContext.SaveChanges();
                return obj.Entity;
            }
			catch (Exception ex)
			{
                return null;
			}
        }

		public RegConfirmation Update(RegConfirmation regCon)
		{
            try
            {
                var obj = _dbContext.RegConfirmations.Update(regCon);
                _dbContext.SaveChanges();
                return obj.Entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public object GetUserWithRole(int? userId, string email, string mobile, string password)
		{
            var query = from u in _dbContext.Users
                        join r in _dbContext.RoleEnrollments on u.Id equals r.UserId
                        join e in _dbContext.Roles on r.RoleId equals e.Id
                        where (userId != null ? u.IsDeleted == false && u.Id == userId : u.IsDeleted == false &&
                        (u.Email == email && u.IsEmailConfirm == true) ||
                        (u.Mobile == mobile && u.IsMobileConfirm == true) && u.Password == password)
                        select new
                        {
                            u.Id,
                            u.Email,
                            u.IsEmailConfirm,
                            e.Title
                        };
                        //select new 
                        //{
                        //    Id = u.Id,
                        //    Email = u.Email,
                        //    Mobile = u.Mobile,
                        //    FullName = u.FullName,
                        //    Password = u.Password,
                        //    UserRole = e.Id,
                        //    RoleName = e.Title,
                        //    IsEmailConfirm = u.IsEmailConfirm,
                        //    IsMobileConfirm = u.IsMobileConfirm
                        //}); 
            //SignUpUserVM data = query.FirstOrDefault();
            return query.FirstOrDefault();
        }

		
	}
}
