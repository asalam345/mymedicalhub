using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Manager.Converter
{
	public class RegConfirmationConverter
	{
		public static List<RegConfirmationVM> FromRegConfirmation(List<RegConfirmation> list)
		{
			List<RegConfirmationVM> vmList = new List<RegConfirmationVM>();
			foreach (RegConfirmation s in list)
			{
				RegConfirmationVM vm = new RegConfirmationVM();
				vm.Id = s.Id;
				vm.UserId = s.UserId;
				vm.Device =s.Device;
				vm.Code = s.Code;
				vmList.Add(vm);
			}
			return vmList;
		}

		public static List<RegConfirmation> FromRegConfirmationVM(List<RegConfirmationVM> vmList)
		{
			List<RegConfirmation> list = new List<RegConfirmation>();
			foreach (RegConfirmationVM vm in vmList)
			{
				RegConfirmation s = new RegConfirmation();
				s.Id = vm.Id;
				s.UserId = vm.UserId;
				s.Device = vm.Device;
				s.Code = vm.Code;
				list.Add(s);
			}
			return list;
		}
	}
}
