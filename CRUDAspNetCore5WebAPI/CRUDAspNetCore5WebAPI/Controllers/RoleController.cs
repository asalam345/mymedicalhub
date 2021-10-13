using Interfaces.Service;
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
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}
		[HttpGet("GetRoles")]
		public Object Get()
		{
			var data = _roleService.GetAllRoles();
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
