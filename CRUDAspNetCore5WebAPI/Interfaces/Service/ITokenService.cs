using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Interfaces.Service
{
	public interface ITokenService
	{
		string BuildToken(string key, string issuer, SignUpUserVM user);
		bool IsTokenValid(string key, string issuer, string token);
	}
}
