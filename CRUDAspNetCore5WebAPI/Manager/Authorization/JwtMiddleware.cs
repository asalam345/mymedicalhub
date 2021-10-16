using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Manager.Helpers;
using Interfaces.Utility;
using Interfaces.Service;

namespace Manager.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next) // IOptions<AppSettings> appSettings
        {
            _next = next;
            //_appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
			try
			{
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = jwtUtils.ValidateJwtToken(token);
                if (userId != null)
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = userService.GetUserByUserId(userId.Value);
                }

                await _next(context);
            }
			catch (System.Exception)
			{

				throw;
			}
            
        }
    }
}