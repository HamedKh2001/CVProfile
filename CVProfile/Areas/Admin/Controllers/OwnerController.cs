﻿using CoreLayer.Dtos;
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
		private readonly IGenericRepository<Owner> _ownergenericRepository;
		public OwnerController(IOwnerService ownerService, IGenericRepository<Owner> ownergenericRepository)
		{
			_ownerService = ownerService;
			_ownergenericRepository = ownergenericRepository;
		}
		public IActionResult Index()
		{
			return View(_ownergenericRepository.GetAll().ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(InsertOwnerDto insertOwnerDto)
		{
			var res = _ownerService.Insertasync(insertOwnerDto).Result;
			if (res.Status == OperationResultStatus.Success)
				return Redirect("/Admin");
			ModelState.AddModelError("FullName", res.Message);
			return View(insertOwnerDto);
		}

		public IActionResult Edit(int id)
		{
			return View(_ownerService.GetProfileasync(id).Result);
		}
		[HttpPost]
		public IActionResult Edit(InsertOwnerDto insertOwnerDto)
		{
			return View();
		}

		[HttpPost]
		public IActionResult ChangeStatus(int id)
		{
			var res = _ownerService.ChangeStatusProfile(id).Result;
			return new JsonResult(res);
		}
	}
}
