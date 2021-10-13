using Domains;
using Interfaces.Service;
using Microsoft.AspNetCore.Http;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleEnrollService _roleEnrollService;

        public UserController(IUserService userService, IRoleEnrollService roleEnrollService)
        {
            _userService = userService;
            _roleEnrollService = roleEnrollService;
        }
       
        //Delete User
        [HttpDelete("DeleteUser")]
        public bool DeleteUser(string UserEmail)
        {
            try
            {
                _userService.DeleteUser(UserEmail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Delete User
        [HttpPut("UpdateUser")]
        public bool UpdateUser(SignUpUserVM userVM)
        {
            try
            {
                _userService.UpdateUser(userVM);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //GET All User
        [HttpGet("GetAllUsers")]
        public Object GetAllUsers()
        {
            var data = _userService.GetAllUsers();
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