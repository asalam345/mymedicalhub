using Manager.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Consumes("application/json")]
	[Authorize(roles: "Doctor")]
	public class DoctorController : ControllerBase
	{
		[HttpGet]
		public Object Get()
		{
			List<string> doctors = new List<string>()
			{
				"Ahmad",
				"A. Karim",
				"Muhaiminul Islam"
			};
			var json = JsonConvert.SerializeObject(doctors, Formatting.Indented,
			   new JsonSerializerSettings()
			   {
				   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			   });
			return json;
		}
	}
}
