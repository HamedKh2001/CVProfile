using System.ComponentModel.DataAnnotations;

namespace CVProfile.Models
{
	public class LoginViewModel
	{
		[Display(Name = "شماره تلفن")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string PhoneNumber { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.Password)]
		public string PassWord { get; set; }
		[Display(Name = "من را بخاطر بسپار")]
		public bool RememberMe { get; set; }
	}
}
