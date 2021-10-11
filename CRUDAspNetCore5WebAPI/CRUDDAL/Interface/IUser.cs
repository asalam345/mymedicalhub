using CRUD_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DAL.Interface
{
	public interface IUser
	{
		User IsExists(User user);
		bool IsCodeExists(RegConfirmation regCon);
		Task<RegConfirmation> Create(RegConfirmation regCon);
		RegConfirmation Update(RegConfirmation regCon);
		SignUpUserVM GetUserWithRole(int? userId, string emailOrMobile, string password);
	}
}
