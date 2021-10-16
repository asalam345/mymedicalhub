using Manager.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace CRUDAspNetCore5WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Consumes("application/json")]
	[IsUserValid]
	public class DashboardController : ControllerBase
	{
		[HttpGet("page")]
		public Object page()
		{
			var data = new string[2];
			var currentUser = (SignUpUserVM)HttpContext.Items["User"];
			data[0] = currentUser.FullName;
			
			string role = currentUser.RoleModel.Title;
			switch (role)
			{
				case "Doctor":
					data[1] = "Doctor";
					break;
				case "Patient":
					data[1] = "Patient";
					break;
				default: return Ok("This Secured Data is available only for Authenticated Users."); 

			}

			var json = JsonConvert.SerializeObject(data, Formatting.Indented,
				new JsonSerializerSettings()
				{
					ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
				}
			);
			return json;
		}
		
	}
}
