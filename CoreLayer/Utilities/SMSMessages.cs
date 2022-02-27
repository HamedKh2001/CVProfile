using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
	public class SMSMessages
	{
		private static string _code { get; set; }
		public SMSMessages(string Code)
		{
			_code = Code;
		}
		public string SignIn
		{
			get { return $"کد تایید شما برای ورود : {_code}"; }
		}
		public string RecoverPassWord
		{
			get { return $"کد بازیابی شما برای ورود : {_code}"; }
		}
	}
}
