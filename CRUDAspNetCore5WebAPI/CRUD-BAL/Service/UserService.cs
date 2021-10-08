using CRUD_DAL.Interface;
using CRUD_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_BAL.Service
{
    public class UserService
    {
        private readonly IRepository<User> _user;

        public UserService(IRepository<User> user)
        {
            _user = user;
        }
        //Get User Details By User Id
        public IEnumerable<User> GetUserByUserId(int userId)
        {
            return _user.GetAll().Where(x => x.Id == userId).ToList();
        }
        //GET All Perso Details 
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _user.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get User by User Name
        public User GetUserByEmailOrMobileAndPassword(string emailOrMobile, string password)
        {
            return _user.GetAll().Where(x => (x.UserEmail == emailOrMobile || x.Mobile == emailOrMobile) && x.UserPassword == password).FirstOrDefault();
        }
        //Add User
        public async Task<User> AddUser(User User)
        {
            return await _user.Create(User);
        }
        //Delete User 
        public bool DeleteUser(string UserEmail)
        {

            try
            {
                var DataList = _user.GetAll().Where(x => x.UserEmail == UserEmail).ToList();
                foreach (var item in DataList)
                {
                    _user.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        //Update User Details
        public bool UpdateUser(User user)
        {
            try
            {
                var DataList = _user.GetAll().Where(x => x.IsDeleted != true).ToList();
                foreach (var item in DataList)
                {
                    _user.Update(item);
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