using Manager.Utility;
using Interfaces.Service;
using Interfaces.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using Manager.Authorization;

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

        public AuthController(IUserService userService, IRoleEnrollService roleEnrollService, 
            IMailService mailService)
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
                if (result.Result)
                {
                    userVm.Id = result.Id;
                    RoleEnrollmentVM roleEnroll = new RoleEnrollmentVM()
                    {
                        UserId = result.Id,
                        RoleId = userVm.RoleModel.Id
                    };
                    await _roleEnrollService.AddRoleEnrollment(roleEnroll);
                    string code = RandomService.RandomPassword();
                    RegConfirmationVM regCon = new RegConfirmationVM()
                    {
                        UserId = result.Id,
                        Device = 'E',
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
                }
                var json = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
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
            SignUpUserVM result;
            if (_mailService.EmailIsValid(user.Email)) {
                result = _userService.GetUserByEmailOrMobileAndPassword(user.Email, null, user.Password);
                result.Device = 'E';
            }
            else
			{
                result = _userService.GetUserByEmailOrMobileAndPassword(null, user.Mobile, user.Password);
                result.Device = 'M';
            }

            var json = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return json;
        }
        [HttpPost("CheckValidation")]
        public Object CheckValidation(SignUpUserVM signUpUser)
		{
            bool result = false;
            if (string.IsNullOrEmpty(signUpUser.Email)) return null;
            bool isEmail = _mailService.EmailIsValid(signUpUser.Email);
            SignUpUserVM userVm = _userService.GetUserByEmailOrMobile(signUpUser.Email, isEmail);
           
            RegConfirmationVM regCon = new RegConfirmationVM()
            {
                UserId = userVm.Id,
                Device = signUpUser.Device,
                Code = signUpUser.ConfirmCode
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
                    result = true;
                }
            }

            var json = JsonConvert.SerializeObject(result, Formatting.Indented,
               new JsonSerializerSettings()
               {
                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
               });

            return json;

        }
    }
}
