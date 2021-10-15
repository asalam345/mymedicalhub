using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Manager.Converter
{
	public class RoleEnrollmentConverter
	{
		public static List<RoleEnrollmentVM> FromRoleEnrollment(List<RoleEnrollment> list)
		{
			List<RoleEnrollmentVM> vmList = new List<RoleEnrollmentVM>();
			foreach (RoleEnrollment s in list)
			{
				RoleEnrollmentVM vm = new RoleEnrollmentVM();
				vm.Id = s.Id;
				vm.UserId = s.UserId;
				vm.RoleId =s.RoleId;
				vmList.Add(vm);
			}
			return vmList;
		}

		public static List<RoleEnrollment> FromRoleEnrollmentVM(List<RoleEnrollmentVM> vmList)
		{
			List<RoleEnrollment> list = new List<RoleEnrollment>();
			foreach (RoleEnrollmentVM vm in vmList)
			{
				RoleEnrollment s = new RoleEnrollment();
				s.Id = vm.Id;
				s.UserId = vm.UserId;
				s.RoleId = vm.RoleId;
				list.Add(s);
			}
			return list;
		}
	}
}
