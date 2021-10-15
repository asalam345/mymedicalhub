using Interfaces.Service;
using Manager.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Authorize]
    public class ProfileController : ControllerBase
	{
        private readonly IUserService _userService;
        public ProfileController(IUserService userService)
		{
            _userService = userService;
        }
       
    }
}
