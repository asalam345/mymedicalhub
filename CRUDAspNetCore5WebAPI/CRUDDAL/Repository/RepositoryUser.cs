﻿using CRUD_DAL.Data;
using CRUD_DAL.Interface;
using CRUD_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DAL.Repository
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
                return _dbContext.Users.Where(x => x.IsDeleted == false).ToList();
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
            return _dbContext.Users.Where(x => x.Email == user.Email && x.Mobile == user.Mobile).FirstOrDefault();
        }

		public bool IsCodeExists(RegConfirmation regCon)
		{
            var row = _dbContext.RegConfirmations.Where(w => w.UserId == regCon.UserId && w.Device == regCon.Device && w.Code == regCon.Code).FirstOrDefault();
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
        public SignUpUserVM GetUserWithRole(int? userId, string emailOrMobile, string password)
		{
            var query = from u in _dbContext.Users
                        join r in _dbContext.RoleEnrollments on u.Id equals r.UserId
                        join e in _dbContext.Roles on r.RoleId equals e.Id
                        where (userId != null ? u.IsDeleted == false && u.Id == userId : u.IsDeleted == false &&
                        (u.Email == emailOrMobile && u.IsEmailConfirm == true) || 
                        (u.Mobile == emailOrMobile && u.IsMobileConfirm == true) && u.Password == password)
                        select new SignUpUserVM()
                        {
                            id = u.Id,
                            email = u.Email,
                            mobile = u.Mobile,
                            fullname = u.FullName,
                            password = u.Password,
                            userRole = e.Id,
                            roleName = e.Title
                        };
            SignUpUserVM data = query.FirstOrDefault();
            return data;
        }

		
	}
}
