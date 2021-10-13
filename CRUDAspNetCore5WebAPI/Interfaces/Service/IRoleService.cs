using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Interfaces.Service
{
	public interface IRoleService
	{
		IEnumerable<RoleVM> GetRoleById(int roleId);
		IEnumerable<RoleVM> GetAllRoles();
		Task<RoleVM> AddRole(RoleVM role);
		bool DeleteRole(int id);
		bool UpdateRole(RoleVM roleVM);
	}
}
