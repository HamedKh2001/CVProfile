using CoreLayer.Dtos;
using CoreLayer.Hubs;
using CoreLayer.IServices;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CVProfile.Controllers
{
	[Authorize]
	public class ContactController : Controller
	{
		#region DI
		private readonly IOrderService _orderService;
		private readonly IRecaptcha _recaptcha;
		private readonly IFileManager _fileManager;
		public static int _progress { get; set; }
		public static string _fileName { get; set; }

		public ContactController(IOrderService orderService, IRecaptcha recaptcha, IFileManager fileManager)
		{
			_orderService = orderService;
			_recaptcha = recaptcha;
			_fileManager = fileManager;
		}
		#endregion


		[HttpPost]
		public IActionResult InsertOrder(ContactDto contactDto, CancellationToken cancellationToken)
		{
			if (_recaptcha.IsSatisfy().Result)
			{
				if (ModelState.IsValid)
				{
					//var cancellationToken = new CancellationTokenSource();
					//cancellationToken.CancelAfter(60000);
					return new JsonResult(_orderService.InsertOrderasync(contactDto,User, cancellationToken).Result);
				}
				return new JsonResult(OperationResault.Error("فیلد ها را وارد کنید"));
			}
			return new JsonResult(OperationResault.Error("!!!مشکل در ریکپتچا"));
		}
		[HttpGet]
		public IActionResult Upload()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
		{
			if (file.Length > 0)
			{
				_fileName = await _fileManager.SaveProgress(file, RootFile.InsertOrderFile, cancellationToken);
				ViewBag.FileName = _fileName;
			}
			return View();
		}
	}
}
