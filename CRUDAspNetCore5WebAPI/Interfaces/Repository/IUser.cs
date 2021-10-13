using Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
	public interface IUser
	{
		User IsExists(User user);
		bool IsCodeExists(RegConfirmation regCon);
		Task<RegConfirmation> Create(RegConfirmation regCon);
		RegConfirmation Update(RegConfirmation regCon);
		object GetUserWithRole(int? userId, string email, string mobile, string password);
	}
}
