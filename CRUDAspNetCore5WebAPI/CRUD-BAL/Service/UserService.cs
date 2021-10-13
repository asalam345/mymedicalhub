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
    public class UserService:IUserService
    {
        private readonly IRepository<User> _user;
        private readonly IUser _iUser;
        private readonly IRepository<RoleEnrollment> _roleEnroll;
        private readonly IRepository<Role> _role;

        public UserService(IRepository<User> user, IUser iUser, IRepository<RoleEnrollment> roleEnroll, IRepository<Role> role)
        {
            _user = user;
            _iUser = iUser;
            _roleEnroll = roleEnroll;
            _role = role;
        }
        //Get User Details By User Id
        public SignUpUserVM GetUserByUserId(int userId)
        {
            var user = _user.GetById(userId);
            var list = new List<User>() { user };
            var userVm = UserConverter.FromUser(list);
            return userVm.FirstOrDefault();
        }
        //GET All Perso Details 
        public IEnumerable<SignUpUserVM> GetAllUsers()
        {
            try
            {
                var list = _user.GetAll().ToList();
                var userVm = UserConverter.FromUser(list);
                return userVm;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get User by User Name
        public SignUpUserVM GetUserByEmailOrMobileAndPassword(string email, string  mobile, string password)
        {
			try
			{
                User user = _user.GetAll().Where(w => 
                ((w.Email == email && w.IsEmailConfirm == true) ||
                (w.Mobile == mobile && w.IsMobileConfirm == true))
                && w.IsDeleted == false
                && w.Password == password).FirstOrDefault();
                RoleEnrollment roleEnrollment = _roleEnroll.GetAll().Where(w => w.UserId == user.Id).FirstOrDefault();
                Role role = _role.GetById(roleEnrollment.RoleId);
                //var obj = _iUser.GetUserWithRole(null, email, mobile, password);
                
                SignUpUserVM userVM = new SignUpUserVM()
				{
					Id = user.Id,
					Email = user.Email,
					Mobile = user.Mobile,
					FullName = user.FullName,
					Password = user.Password,
					IsEmailConfirm = user.IsEmailConfirm,
					IsMobileConfirm = user.IsMobileConfirm,
					UserRole = role.Id,
					RoleName = role.Title
				};
                //return (SignUpUserVM)obj;
                return userVM;
            }
			catch (Exception ex)
			{

                return null;
			}
            
        }
        //Add User
        public async Task<SignUpUserVM> AddUser(SignUpUserVM userVm)
        {
            User user = new User
            {
                Email = userVm.Email,
                FullName = userVm.FullName,
                Mobile = userVm.Mobile,
                Password = userVm.Password
            };
            var existUser = _iUser.IsExists(user);
            if (existUser == null)
            {
                var createUser = await _user.Create(user);
                if (createUser != null)
                {
                    userVm.Id = user.Id;
                    return userVm;
                }
            }
            return null;
        }
        public async Task<RegConfirmationVM> AddCode(RegConfirmationVM regConVM)
        {
            var vmList = new List<RegConfirmationVM>() { regConVM };
            RegConfirmation regCon = RegConfirmationConverter.FromRegConfirmationVM(vmList).FirstOrDefault();
            RegConfirmationVM result = new RegConfirmationVM();
            List<RegConfirmation> list = new List<RegConfirmation>();
            if (_iUser.IsCodeExists(regCon))
			{
                var updatedValue = _iUser.Update(regCon);
                list = new List<RegConfirmation>() { updatedValue };
                result = RegConfirmationConverter.FromRegConfirmation(list).FirstOrDefault();
                return result;
            }
            var createdValue = await _iUser.Create(regCon);
            list = new List<RegConfirmation>() { createdValue };
            result = RegConfirmationConverter.FromRegConfirmation(list).FirstOrDefault();
            return result;
        }
        public bool DeviceConfirm(RegConfirmationVM regConVM)
        {
            var vmList = new List<RegConfirmationVM>() { regConVM };
            var regCon = RegConfirmationConverter.FromRegConfirmationVM(vmList).FirstOrDefault();
            if (regCon == null) return false;
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
            catch (Exception ex)
            {
                return true;
            }

        }
        public bool UpdateUser(SignUpUserVM userVM)
        {
            try
            {
                var vmList = new List<SignUpUserVM>() { userVM };
                var user = UserConverter.FromUserVM(vmList).FirstOrDefault();
                _user.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
	}
}