using CoreLayer.Dtos;
using CoreLayer.IServices;
using CoreLayer.Services.FileManager;
using CORETest.Utilities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVProfile.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Owner")]
	public class OwnerController : Controller
	{
		private readonly IOwnerService _ownerService;
		public OwnerController(IOwnerService ownerService)
		{
			_ownerService = ownerService;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(InsertOwnerDto insertOwnerDto)
		{
			var res = _ownerService.Insert(insertOwnerDto).Result;
			if (res.Status == OperationResultStatus.Success)
				return Redirect("/Admin");
			ModelState.AddModelError("FullName", res.Message);
			return View(insertOwnerDto);
		}

	}
}
