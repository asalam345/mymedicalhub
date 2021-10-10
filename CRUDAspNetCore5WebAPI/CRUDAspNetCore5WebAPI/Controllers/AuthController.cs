using CRUD_BAL.Service;
using CRUD_DAL.Models;
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
	public class AuthController : ControllerBase
	{
        private readonly UserService _userService;
        private readonly RoleEnrollService _roleEnrollService;
        private readonly IMailService _mailService;

        public AuthController(UserService userService, RoleEnrollService roleEnrollService, IMailService mailService)
		{
            _userService = userService;
            _roleEnrollService = roleEnrollService;
            _mailService = mailService;
        }

        [HttpPost("AddUser")]
        public async Task<Object> AddUser(SignUpUserVM userVm)
        {
            try
            {
                var result = await _userService.AddUser(userVm);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    userVm.id = result.id;
                    RoleEnrollment roleEnroll = new RoleEnrollment()
                    {
                        UserId = result.id,
                        RoleId = userVm.userRole
                    };
                    await _roleEnrollService.AddRoleEnrollment(roleEnroll);
                    string code = RandomService.RandomPassword();
                    RegConfirmation regCon = new RegConfirmation()
                    {
                        UserId = result.id,
                        Device = 'M',
                        Code = code
                    };
                    var codeResult = await _userService.AddCode(regCon);
                    if (codeResult == null)
                    {
                        return false;
                    }
                    MailRequest request = new MailRequest()
                    {
                        ToEmail = result.email,
                        Subject = "Your Account Confirmation Email",
                        Body = @"<h1>Welcome " + userVm.fullname + "!</h1> <h3> Your Confrimation Code Is " + code + "</h3> <br/><p>Thanks</p><p>My Medical HUB</p><p style='color:red;'>If your don't registed to mymedicalhub.com. Please ignore this mail!</p>"
                    };
                    await _mailService.SendEmailAsync(request);
                    //EmailController email = new EmailController(_mailService);
                    //await email.Send(request);
                }
                var json = JsonConvert.SerializeObject(userVm, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
                return json;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        [HttpPost]
        public Object LoginUser(UserVM user)
        {
            var data = _userService.GetUserByEmailOrMobileAndPassword(user.emailOrMobile, user.password);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }
        [HttpPost("CheckValidation")]
        public Object CheckValidation(SignUpUserVM userVm)
		{
            RegConfirmation regCon = new RegConfirmation()
            {
                UserId = userVm.id,
                Device = userVm.device,
                Code = userVm.confirmCode
            };
			if (_userService.DeviceConfirm(regCon))
			{
                User user = new User()
                {
                    Id = userVm.id,
                    Email = userVm.email,
                    Mobile = userVm.mobile,
                    Password = userVm.password,
                    IsEmailConfirm = true
                };
                var updateUser = _userService.UpdateUser(user);
                if(updateUser)
				{
                    return LoginUser(new UserVM() { emailOrMobile = userVm.email, password = userVm.password, role = userVm.role });
                }
            }

            return null;

        }
    }
}
