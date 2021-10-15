using Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Interfaces.Service
{
	public interface IUserService
	{
		SignUpUserVM GetUserByUserId(int userId);
		IEnumerable<SignUpUserVM> GetAllUsers();
		SignUpUserVM GetUserByEmailOrMobileAndPassword(string email, string mobile, string password);
		Task<SignUpUserVM> AddUser(SignUpUserVM userVm);
		Task<RegConfirmationVM> AddCode(RegConfirmationVM regCon);
		bool DeviceConfirm(RegConfirmationVM regCon);
		bool DeleteUser(string UserEmail);
		bool UpdateUser(SignUpUserVM userVM);
		SignUpUserVM GetUserByEmailOrMobile(string emailOrMobile, bool isEmail);
	}
}
