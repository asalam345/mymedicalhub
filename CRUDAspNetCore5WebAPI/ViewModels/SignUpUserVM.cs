using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class SignUpUserVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmCode { get; set; }
        public char Device { get; set; }
        public int UserRole { get; set; }
        public string RoleName { get; set; }
        public bool IsEmailConfirm { get; set; }
        public bool IsMobileConfirm { get; set; }
    }
}
