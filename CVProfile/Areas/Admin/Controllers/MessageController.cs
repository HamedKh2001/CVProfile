using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Entities;
using DataLayer.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVProfile.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Owner")]
	public class MessageController : Controller
	{
		private readonly IMessageService _messageService;
		public MessageController(IMessageService messageService)
		{
			_messageService = messageService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View(_messageService.GetAll().Result);
		}
		
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var res= _messageService.Delete(id).Result;
			return new JsonResult(res);
		}

		[HttpGet]
		public IActionResult DisplayMessage(int id)
		{
			return View(_messageService.Get(id).Result);
		}
	}
}
