using CoreLayer.IServices;
using CORETest.Utilities;
using Kavenegar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class SMS : ISMS
	{
		public OperationResault SendSMS(string PhoneNumber, string Text)
		{
			try
			{
				var sender = "1000596446";
				var receptor = PhoneNumber;
				var message = "کد فعالسازی شما :" + Text;
				var api = new KavenegarApi("386C577978465736345065515A695439436849684455713550623668744F3139684E4B2B644930472F48773D");
				var x = api.Send(sender, receptor, message);
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
	}
}
