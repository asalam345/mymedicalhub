using CRUD_BAL.Utility;
using Interfaces.Service;
using Interfaces.Utility;
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
	public class AuthController : ControllerBase
	{
        private readonly IUserService _userService;
        private readonly IRoleEnrollService _roleEnrollService;
        private readonly IMailService _mailService;

        public AuthController(IUserService userService, IRoleEnrollService roleEnrollService, IMailService mailService)
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
                    userVm.Id = result.Id;
                    RoleEnrollmentVM roleEnroll = new RoleEnrollmentVM()
                    {
                        UserId = result.Id,
                        RoleId = userVm.UserRole
                    };
                    await _roleEnrollService.AddRoleEnrollment(roleEnroll);
                    string code = RandomService.RandomPassword();
                    RegConfirmationVM regCon = new RegConfirmationVM()
                    {
                        UserId = result.Id,
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
                        ToEmail = result.Email,
                        Subject = "Your Account Confirmation Email",
                        Body = @"<h1>Welcome " + userVm.FullName + "!</h1> <h3> Your Confrimation Code Is " + code + "</h3> <br/><p>Thanks</p><p>My Medical HUB</p><p style='color:red;'>If your don't registed to mymedicalhub.com. Please ignore this mail!</p>"
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
        public Object LoginUser(SignUpUserVM user)
        {
            var data = _userService.GetUserByEmailOrMobileAndPassword(user.Email, user.Mobile, user.Password);
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
            RegConfirmationVM regCon = new RegConfirmationVM()
            {
                UserId = userVm.Id,
                Device = userVm.Device,
                Code = userVm.ConfirmCode
            };
			if (_userService.DeviceConfirm(regCon))
			{
                var user = _userService.GetUserByUserId(userVm.Id);
                userVm.Email = user.Email;
                userVm.Mobile = user.Mobile;
                userVm.FullName = user.FullName;
                userVm.Password = user.Password;
                userVm.IsEmailConfirm = true;
                var updateUser = _userService.UpdateUser(userVm);
                if(updateUser)
				{
                    return LoginUser(userVm);
                }
            }

            return null;

        }
    }
}
