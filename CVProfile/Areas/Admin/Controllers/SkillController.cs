using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVProfile.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Owner")]
	public class SkillController : Controller
	{
		private readonly IGenericRepository<Skill> _genericRepository;
		public SkillController(IGenericRepository<Skill> genericRepository)
		{
			_genericRepository = genericRepository;
		}
		public IActionResult Index()
		{
			return View(_genericRepository.GetAll().ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Skill skill)
		{
			if (ModelState.IsValid)
			{
				var Res = _genericRepository.Insertasync(skill).Result;
				if (Res.Status == OperationResultStatus.Success)
				{
					return Redirect("/admin/skill");
				}
				ModelState.AddModelError("Name", Res.Message);
			}
			return View(skill);
		}

		public IActionResult Edit(int Id)
		{
			var res = _genericRepository.Getasync(Id).Result;
			if (res ==null)
			{
				return NotFound();
			}
			return View(res);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Skill skill)
		{
			if (ModelState.IsValid)
			{
				var Res = _genericRepository.Update(skill);
				if (Res.Status == OperationResultStatus.Success)
				{
					return Redirect("/admin/skill");
				}
				ModelState.AddModelError("Name", Res.Message);
			}
			return View(skill);
		}

		public IActionResult Delete(int Id)
		{
			var res = _genericRepository.Getasync(Id).Result;
			if (res == null)
			{
				return NotFound();
			}
			return View(res);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Skill skill)
		{
				var Gotskill = _genericRepository.Getasync(skill.Id).Result;
				var Res = _genericRepository.Delete(Gotskill);
				if (Res.Status == OperationResultStatus.Success)
				{
				return Redirect("/admin/skill");
			}
				ModelState.AddModelError("Name", Res.Message);
			return View(skill);
		}
	}
}
