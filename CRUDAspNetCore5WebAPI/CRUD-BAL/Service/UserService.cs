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
        private readonly IUser _iUser;


        public UserService(IRepository<User> user, IUser iUser)
        {
            _user = user;
            _iUser = iUser;
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
            return _user.GetAll().Where(x => ((x.Email == emailOrMobile && x.IsEmailConfirm == true) || (x.Mobile == emailOrMobile && x.IsMobileConfirm == true)) && x.Password == password).FirstOrDefault();
        }
        //Add User
        public async Task<SignUpUserVM> AddUser(SignUpUserVM userVm)
        {
            User user = new User
            {
                Email = userVm.email,
                FullName = userVm.fullname,
                Mobile = userVm.mobile,
                Password = userVm.password
            };
            var existUser = _iUser.IsExists(user);
            if (existUser == null)
            {
                var createUser = await _user.Create(user);
                if (createUser != null)
                {
                    userVm.id = user.Id;
                    return userVm;
                }
            }
            return null;
        }
        public async Task<RegConfirmation> AddCode(RegConfirmation regCon)//string code, int userId, char device
        {
            if(_iUser.IsCodeExists(regCon))
			{
                return _iUser.Update(regCon);
            }
            return await _iUser.Create(regCon);
        }
        public bool DeviceConfirm(RegConfirmation regCon)
        {
            return _iUser.IsCodeExists(regCon);
        }
        //Delete User 
        public bool DeleteUser(string UserEmail)
        {
            try
            {
                var DataList = _user.GetAll().Where(x => x.Email == UserEmail).ToList();
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
                _user.Update(user);
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}