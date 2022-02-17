using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static DataLayer.Entities.Person;

namespace CoreLayer.Dtos
{
	public class InsertOwnerDto
	{
		[Display(Name ="نام نام خانوادگی")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string FullName { get; set; }
		[Display(Name ="شماره تلفن")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Phonenumber { get; set; }
		[Display(Name = "تصویر")]
		public IFormFile ProfilePhoto { get; set; }
		[Display(Name = "شهر")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string City { get; set; }
		[Display(Name = "زبانها")]
		public string Languages { get; set; }
		[Display(Name = "درباره من")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.MultilineText)]
		public string About { get; set; }
		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Email { get; set; }
		[Display(Name = "سال تولد")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }
	}
}
