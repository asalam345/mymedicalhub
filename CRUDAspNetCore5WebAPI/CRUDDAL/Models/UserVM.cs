using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_DAL.Models
{
    public class UserVM
    {
        public string emailOrMobile { get; set; }
        public string password { get; set; }
        public int role { get; set; }
    }
    public class SignUpUserVM
    {
        public int id { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public string confirmCode { get; set; }
        public char device { get; set; }
        //public int role { get; set; }
        public int userRole { get; set; }
        public string roleName { get; set; }
    }
}
