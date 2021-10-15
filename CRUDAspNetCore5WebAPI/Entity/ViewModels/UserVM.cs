using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string EmailOrMobile { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
