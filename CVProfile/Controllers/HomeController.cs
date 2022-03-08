using CVProfile.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVProfile.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			//var cookie =new Microsoft.AspNetCore.Http.CookieOptions();
			//cookie.Expires = DateTimeOffset.Now.AddSeconds(5);
			//HttpContext.Response.Cookies.Append("ReSend","First", cookie);
			//HttpContext.Session.Set("ReSend",Encoding.ASCII.GetBytes(DateTime.Now.TimeOfDay.ToString()));
			//byte[] array;
			//var res = HttpContext.Session.Keys.ToArray();
			//var ress = HttpContext.Session.TryGetValue("ReSend",out array);
			//var str = Convert.ToDateTime(Encoding.Default.GetString(array));
			//var def = DateTime.Now - str;
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
