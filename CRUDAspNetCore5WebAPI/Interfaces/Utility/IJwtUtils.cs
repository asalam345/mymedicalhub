using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Interfaces.Utility
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(SignUpUserVM user);
        public int? ValidateJwtToken(string token);
    }
}
