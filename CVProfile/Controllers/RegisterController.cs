using CoreLayer.IServices;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using CVProfile.Models;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CVProfile.Controllers
{
	public class RegisterController : Controller
	{
		#region DI
		private IFileManager _fileManager;
		private readonly IUserService _userService;
		private readonly ISMS _sms;
		private readonly IRecaptcha _recaptcha;

		public RegisterController(IFileManager fileManager, IUserService userService, ISMS sms, IRecaptcha recaptcha)
		{
			_fileManager = fileManager;
			_userService = userService;
			_sms = sms;
			_recaptcha = recaptcha;
		}
		#endregion

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
				return View(loginViewModel);

			var user = _userService.ValideUser(loginViewModel.PhoneNumber, loginViewModel.PassWord);

			if (user == null)
			{
				ModelState.AddModelError("PhoneNumber", "نام کاربری یا رمز عبور اشتباه است");
				return View();
			}

			List<Claim> Claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Role , user.Role.ToString()),
				new Claim(ClaimTypes.NameIdentifier , loginViewModel.PhoneNumber),
				new Claim(ClaimTypes.Name , user.FullName),
				new Claim("UserID" , user.Id.ToString())
			};
			var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var Claimprincipal = new ClaimsPrincipal(Identity);
			var prop = new AuthenticationProperties()
			{
				IsPersistent = loginViewModel.RememberMe
			};
			HttpContext.SignInAsync(Claimprincipal, prop);
			var url = HttpContext.Request.Query["ReturnUrl"];
			if (url.Count != 0)
			{
				return Redirect(url);
			}
			return Redirect("/");
		}

		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SignUp(SignUpViewModel signUpViewModel)
		{
			if (ModelState.IsValid)
			{
				if (_recaptcha.IsSatisfy().Result)
				{
					if (_userService.ExistUser(signUpViewModel.PhoneNumber))
					{
						ModelState.AddModelError("PhoneNumber", "کاربری با این شماره تلفن موجود می باشد");
						return View(signUpViewModel);
					}
					var Rand = new Random();
					var newRand = Rand.Next(1000, 9999).ToString();
					string PhotoName = null;
					if (signUpViewModel.ProfilePhoto != null)
					{
						PhotoName = _fileManager.SaveFile(signUpViewModel.ProfilePhoto, RootFile.InsertUserPhoto);
					}
					var user = new User()
					{
						FullName = signUpViewModel.FullName,
						IsActive = false,
						Phonenumber = signUpViewModel.PhoneNumber,
						ProfilePhoto = PhotoName,
						ActivateCode = newRand,
						PassWord = signUpViewModel.PassWord,
						Role = Person.Roles.User
					};

					var smsResault = _sms.SendSMS(signUpViewModel.PhoneNumber, "کد فعالسازی شما :" + newRand).Result;
					if (smsResault.Status == OperationResultStatus.Error)
					{
						ModelState.AddModelError("PhoneNumber", smsResault.Message);
					}
					var signUpResault = _userService.SignUpUser(user);
					if (signUpResault.Status == OperationResultStatus.Success)
					{
						return Redirect($"/Activation/{signUpViewModel.PhoneNumber}");
					}
					ModelState.AddModelError("PhoneNumber", signUpResault.Message);
					return View(signUpViewModel);
				}
				ModelState.AddModelError("PhoneNumber", "مشکل در ریکپتچا گوگل");
			}
			return View(signUpViewModel);
		}

		[HttpGet("Activation/{phonenumber}")]
		public IActionResult ActivationUser()
		{
			return View();
		}

		[HttpPost("Activation/{phonenumber}")]
		public IActionResult ActivationUser(string phonenumber, string password)
		{
			var res = _userService.ActivateUser(phonenumber, password);
			if (res.Status == OperationResultStatus.Success)
				return new JsonResult(res);
			if (res.Status == OperationResultStatus.NotFound)
				return new JsonResult(res);
			ModelState.AddModelError("password", res.Message);
			return View(password);
		}

		public IActionResult AccessDenied()
		{
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.SignOutAsync();
			return Redirect("/");
		}

		[HttpPost]
		public IActionResult ResendSms(string phonenumber)
		{
			if (!IsExistSession("ReSend"))
				SetResendsmsTime();
			else
			{
				var remainedTime = GetResendsmsTime();
				if (remainedTime < TimeSpan.FromMinutes(3))
					return new JsonResult(OperationResault.Error($"تا ارسال پیام بعد 3 دقیقه صبر کنید"));
			}
			var user = _userService.GetUserByphoneNumber(phonenumber);
			if (user != null)
				return new JsonResult(_sms.SendSMS(phonenumber, user.ActivateCode).Result);
			return new JsonResult(OperationResault.Error("کاربری یافت نشد"));
		}

		[HttpPost]
		public IActionResult ForgetPassword(string phonenumber)
		{
			if (!IsExistSession("ReSend"))
				SetResendsmsTime();
			else
			{
				var remainedTime = GetResendsmsTime();
				if (remainedTime < TimeSpan.FromMinutes(3))
					return new JsonResult(OperationResault.Error($"تا ارسال پیام بعد 3 دقیقه صبر کنید"));
			}
			var res = _userService.RecoverUser(phonenumber);
			var smsRes = _sms.SendSMS(phonenumber, res.Message).Result;
			if (res.Status == OperationResultStatus.Success && smsRes.Status == OperationResultStatus.Success)
				return new JsonResult(OperationResault.Success("رمز عبور جدید با موفقیت ارسال شد"));
			if (smsRes.Status != OperationResultStatus.Success)
				return new JsonResult(res);
			return new JsonResult(res);
		}

		private void SetResendsmsTime()
		{
			HttpContext.Session.Set("ReSend", Encoding.ASCII.GetBytes(DateTime.Now.TimeOfDay.ToString()));
		}
		private TimeSpan GetResendsmsTime()
		{
			byte[] array;
			HttpContext.Session.TryGetValue("ReSend", out array);
			var str = Convert.ToDateTime(Encoding.Default.GetString(array));
			return DateTime.Now - str;
		}
		private bool IsExistSession(string key)
		{
			var keys = HttpContext.Session.Keys.ToList();
			return keys.Any(k => k.Contains(key));
		}
	}
}

