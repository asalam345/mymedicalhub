using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class SignUpUserVM
    {
        public SignUpUserVM()
		{
            RoleModel = new RoleVM();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmCode { get; set; }
        public char Device { get; set; }
        public bool IsEmailConfirm { get; set; }
        public bool IsMobileConfirm { get; set; }
        public bool IsDelete { get; set; }
        public RoleVM RoleModel { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Result { get; set; }
        //public RoleEnum Role { get; set; }
    }
}
