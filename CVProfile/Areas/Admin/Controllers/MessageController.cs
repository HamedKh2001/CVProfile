using CoreLayer.IServices;
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
		
		[HttpGet]
		public IActionResult Delete(int id)
		{
			return View();
		}

		[HttpGet]
		public IActionResult DisplayMessage(int id)
		{
			return View(_messageService.Get(id).Result);
		}
	}
}
