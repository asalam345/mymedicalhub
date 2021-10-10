using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult SignUp()
		{
			return View();
		}
		public IActionResult CheckUserValidation()
		{
			return View();
		}
	}
}
