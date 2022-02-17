using CoreLayer.Dtos;
using CoreLayer.IServices;
using CoreLayer.Services.FileManager;
using CORETest.Utilities;
using CVProfile.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVProfile.Controllers
{
	[Authorize]
	public class ContactController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IRecaptcha _recaptcha;
		public ContactController(IOrderService orderService , IRecaptcha recaptcha)
		{
			_orderService = orderService;
			_recaptcha = recaptcha;
		}
		[HttpPost]
		public IActionResult InsertOrder(ContactDto contactDto)
		{
			if (_recaptcha.IsSatisfy().Result)
			{
				if (ModelState.IsValid)
				{
					return new JsonResult(_orderService.InsertOrder(contactDto, User));
				}
				return new JsonResult("فیلد ها را وارد کنید");
			}
			return new JsonResult(OperationResault.Error("!!!مشکل در ریکپتچا"));
		}
	}
}
