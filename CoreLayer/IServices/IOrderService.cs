using CoreLayer.Dtos;
using CORETest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IOrderService
	{
		Task<OperationResault> InsertOrderasync(ContactDto contactDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
	}
}
