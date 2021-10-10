using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAspNetCore5WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Consumes("application/json")]
	[Authorize(Roles = "Doctor, Patient")]
	public class DashboardController : ControllerBase
	{
		[HttpGet("GetSchedules")]
		[Authorize(Roles = "Patient")]
		public Object GetSchedules()
		{
			var data = new int [0,1,2];
			var json = JsonConvert.SerializeObject(data, Formatting.Indented,
				new JsonSerializerSettings()
				{
					ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
				}
			);
			return json;
		}
		[HttpGet("Get")]
		[Authorize(Roles = "Doctor")]
		public Object DoctorPage()
		{
			var data = new int[0, 1, 2];
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
