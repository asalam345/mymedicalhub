using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public RoleVM Role { get; set; }
        public string Token { get; set; }
        
        public AuthenticateResponse(SignUpUserVM user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Mobile = user.Mobile;
            Role = user.RoleModel;
            Token = token;
        }
    }
}
