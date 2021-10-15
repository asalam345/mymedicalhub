using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Manager.Converter
{
	public class UserConverter
	{
		public static List<SignUpUserVM> FromUser(List<User> list)
		{
			List<SignUpUserVM> vmList = new List<SignUpUserVM>();
			foreach (User s in list)
			{
				SignUpUserVM vm = new SignUpUserVM();
				vm.Id = s.Id;
				vm.Email = s.Email;
				vm.Mobile = s.Mobile;
				vm.FullName = s.FullName;
				vm.Password = s.Password;
				vmList.Add(vm);
			}
			return vmList;
		}
		public static List<User> FromUserVM(List<SignUpUserVM> vmList)
		{
			List<User> list = new List<User>();
			foreach (SignUpUserVM vm in vmList)
			{
				User s = new User();
				s.Id = vm.Id;
				s.Email = vm.Email;
				s.Mobile = vm.Mobile;
				s.FullName = vm.FullName;
				s.Password = vm.Password;
				s.IsEmailConfirm = vm.IsEmailConfirm;
				list.Add(s);
			}
			return list;
		}
	}
}
