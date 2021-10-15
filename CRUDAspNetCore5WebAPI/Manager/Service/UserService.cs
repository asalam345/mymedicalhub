using Manager.Converter;
using Domains;
using Interfaces.Repository;
using Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Interfaces.Utility;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Manager.Service
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _user;
        private readonly IUser _iUser;
        private readonly IRepository<RoleEnrollment> _roleEnroll;
        private readonly IRepository<Role> _role;
        private IJwtUtils _jwtUtils;
        private readonly IMailService _mailService;

        public UserService(IRepository<User> user, IUser iUser, IRepository<RoleEnrollment> roleEnroll, 
            IRepository<Role> role, IJwtUtils jwtUtils, IMailService mailService)
        {
            _user = user;
            _iUser = iUser;
            _roleEnroll = roleEnroll;
            _role = role;
            _jwtUtils = jwtUtils;
            _mailService = mailService;
        }
        //Get User Details By User Id
        public SignUpUserVM GetUserByUserId(int userId)
        {
            var user = _user.GetById(userId);
            //var list = new List<User>() { user };
            //var userVm = UserConverter.FromUser(list);

            RoleEnrollment roleEnrollment = _roleEnroll.GetAll().Where(w => w.UserId == user.Id).FirstOrDefault();
            Role role = _role.GetById(roleEnrollment.RoleId);

            SignUpUserVM userVM = new SignUpUserVM()
            {
                Id = user.Id,
                Email = user.Email,
                Mobile = user.Mobile,
                FullName = user.FullName,
                Password = user.Password,
                IsEmailConfirm = user.IsEmailConfirm,
                IsMobileConfirm = user.IsMobileConfirm
            };

            if(role != null)
			{
                userVM.RoleModel = new RoleVM { Id = role.Id, Title = role.Title };
            }
            //return userVm.FirstOrDefault();
            return userVM;
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
            SignUpUserVM signUpUserVM = new SignUpUserVM();
            signUpUserVM.Result = false;

            try
            {
                User user = _user.GetAll().Where(w => w.IsDeleted == false && (w.Email == email || w.Mobile == mobile)).FirstOrDefault();
                if (user == null)
                {
                    signUpUserVM.Message = "User not found!";
                    return signUpUserVM; // new AuthenticateResponse(signUpUserVM, null,);
                }
                var isVerified = BCryptNet.Verify(password, user.Password);
                if (!isVerified)
                {
                    signUpUserVM.Message = "Password not match!";
                    return signUpUserVM; // new AuthenticateResponse(null, null, "Password not match!");
                }
                
                if (!string.IsNullOrEmpty(email) && !user.IsEmailConfirm)
				{
                    signUpUserVM.Email = email;
                    signUpUserVM.Message = "Email not confirmed!";
                    return signUpUserVM; // new AuthenticateResponse(signUpUserVM, null, message);
                }
                else if (!string.IsNullOrEmpty(mobile) && !user.IsMobileConfirm)
                {
                    signUpUserVM.Mobile = mobile;
                    signUpUserVM.Message = "Mobile not confirmed!";
                    return signUpUserVM;
                }

                signUpUserVM.Message = "Success";
                signUpUserVM.Result = true;

                RoleEnrollment roleEnrollment = _roleEnroll.GetAll().Where(w => w.UserId == user.Id).FirstOrDefault();
                    Role role = _role.GetById(roleEnrollment.RoleId);

                signUpUserVM.Id = user.Id;
                signUpUserVM.Email = user.Email;
                signUpUserVM.Mobile = user.Mobile;
                signUpUserVM.FullName = user.FullName;
                //signUpUserVM.Password = user.Password;
                signUpUserVM.IsEmailConfirm = user.IsEmailConfirm;
                signUpUserVM.IsMobileConfirm = user.IsMobileConfirm;
                signUpUserVM.RoleModel = new RoleVM { Id = role.Id, Title = role.Title };
                var jwtToken = _jwtUtils.GenerateJwtToken(signUpUserVM);
                signUpUserVM.Token = jwtToken;
                return signUpUserVM; // new AuthenticateResponse(userVM, jwtToken, "Success");
                
            }
			catch (Exception ex)
			{
                return null;
			}
            
        }
        //Add User
        public async Task<SignUpUserVM> AddUser(SignUpUserVM userVm)
        {
            userVm.Result = true;
            var password = BCryptNet.HashPassword(userVm.Password);
            User user = new User
            {
                Email = userVm.Email,
                FullName = userVm.FullName,
                Mobile = userVm.Mobile,
                Password = password
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
			else
			{
                userVm.Message = "User already registered!";
                userVm.Result = false;
            }
            userVm.Password = "";
            return userVm;
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
                return false;
            }
        }
        public SignUpUserVM GetUserByEmailOrMobile(string emailOrMobile, bool isEmail)
		{
            User user;
            if (isEmail)
                user = _user.GetAll().Where(w => w.IsDeleted == false && w.Email == emailOrMobile).FirstOrDefault();
            else
                user = _user.GetAll().Where(w => w.IsDeleted == false && w.Mobile == emailOrMobile).FirstOrDefault();

            SignUpUserVM userVM = new SignUpUserVM();
            userVM.Id = user.Id;
            userVM.Email = user.Email;
            userVM.Mobile = user.Mobile;
            userVM.IsEmailConfirm = user.IsEmailConfirm;
            userVM.IsMobileConfirm = user.IsMobileConfirm;
            
            return userVM;
        }

    }
}