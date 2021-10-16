using Interfaces.Service;
using Interfaces.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace CRUDAspNetCore5WebAPI.Controllers
{
	public class EmailController : Controller
	{
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost("ConfirmationEmailSend")]
        public async Task<IActionResult> ConfirmationEmailSend(SignUpUserVM user, string code)
        {
            try
            {
                await mailService.SendConfirmationEmailAsync(user, code);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
