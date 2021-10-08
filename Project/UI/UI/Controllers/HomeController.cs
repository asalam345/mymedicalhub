using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public JsonResult Login(string user, string password)
        {
            string apiUrl = "UserDetails";// /LoginUser
            UserVM ud = new UserVM { emailOrMobile= user, password = password };
            DataTable responseObj = ApiCall.PostInfo(ud, apiUrl).Result;
            return Json(new { name="", role= ""});
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.User = null;
            string apiUrl = "https://localhost:44362/api/UserDetails/GetAllUsers";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    //Stream receiveStream = response.GetResponseStream();
                    
                    var data = await response.Content.ReadAsStringAsync();
                    //StreamReader readStream = new StreamReader(data, Encoding.UTF8);
                    //var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //return Json(data);
                    var newString = data.Replace(@"\r\n", "");
                    ViewBag.User = newString;

                }


            }
            return View();

        }
        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
