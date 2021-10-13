using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Interfaces.Service
{
	public interface IRoleEnrollService
	{
		IEnumerable<RoleEnrollmentVM> GetRoleEnrollmentByRoleEnrollmentId(int userId);
		IEnumerable<RoleEnrollmentVM> GetAllRoleEnrollments();
		Task<RoleEnrollmentVM> AddRoleEnrollment(RoleEnrollmentVM RoleEnrollment);
		bool DeleteRoleEnrollment(int id);
		bool UpdateRoleEnrollment(RoleEnrollmentVM user);
	}
}
