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
	[Authorize(roles: "Patient")]
	public class PatientController : ControllerBase
	{
		[HttpGet]
		public Object Get()
		{
			List<string> patient = new List<string>()
			{
				"Bilkis",
				"Azad Khan",
				"Joynul Abidin"
			};
			var json = JsonConvert.SerializeObject(patient, Formatting.Indented,
			   new JsonSerializerSettings()
			   {
				   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			   });
			return json;
		}
	}
}
