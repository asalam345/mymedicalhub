using CRUD_BAL.Service;
using CRUD_DAL.Interface;
using CRUD_DAL.Models;
using Microsoft.AspNetCore.Http;
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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly RoleEnrollService _roleEnrollService;

        public UserController(UserService userService, RoleEnrollService roleEnrollService)
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
        public bool UpdateUser(User Object)
        {
            try
            {
                _userService.UpdateUser(Object);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        //GET All User by Name
        //[HttpGet("GetAllUserByName")]
        //public Object GetAllUserByName(string emailOrMobile, string password)
        //{
        //    var data = _userService.GetUserByEmailOrMobileAndPassword(emailOrMobile, password);
        //    var json = JsonConvert.SerializeObject(data, Formatting.Indented,
        //        new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //        }
        //    );
        //    return json;
        //}

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