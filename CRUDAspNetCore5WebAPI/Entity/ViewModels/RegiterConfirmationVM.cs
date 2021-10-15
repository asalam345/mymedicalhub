using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
	public class RegConfirmationVM
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public char Device { get; set; }
        public string Code { get; set; }
    }
}
