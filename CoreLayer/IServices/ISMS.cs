using CORETest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface ISMS
	{
		OperationResault SendSMS(string PhoneNumber,string Text);
	}
}
