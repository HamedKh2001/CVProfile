﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVProfile.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Owner")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
