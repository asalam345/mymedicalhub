using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Manager.Converter
{
	public class RoleConverter
	{
		public static List<RoleVM> FromRole(List<Role> list)
		{
			List<RoleVM> vmList = new List<RoleVM>();
			foreach (Role s in list)
			{
				RoleVM vm = new RoleVM();
				vm.Id = s.Id;
				vm.Title = s.Title;
				vmList.Add(vm);
			}
			return vmList;
		}

		public static List<Role> FromRoleVM(List<RoleVM> vmList)
		{
			List<Role> list = new List<Role>();
			foreach (RoleVM vm in vmList)
			{
				Role s = new Role();
				s.Id = vm.Id;
				s.Title = vm.Title;
				list.Add(s);
			}
			return list;
		}
	}
}
