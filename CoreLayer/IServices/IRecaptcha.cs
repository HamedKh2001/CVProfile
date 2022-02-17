using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IRecaptcha
	{
		Task<bool> IsSatisfy();
	}
}
