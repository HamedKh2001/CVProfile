using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVProfile.Models
{
	public class SignUpViewModel
	{
		[Display(Name ="نام و نام خانوادگی")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string FullName { get; set; }

		[Display(Name = "شماره تلفن همراه")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string PhoneNumber { get; set; }

		[Display(Name = "تصویر")]
		public IFormFile ProfilePhoto { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.Password)]
		public string PassWord { get; set; }

		[Display(Name = "تکرار رمز عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[Compare("PassWord")]
		[DataType(DataType.Password)]
		public string RePassWord { get; set; }
	}
}
