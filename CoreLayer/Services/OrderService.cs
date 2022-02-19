using CoreLayer.Dtos;
using CoreLayer.IServices;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class OrderService : IOrderService
	{
		private readonly CVContext _context;
		private readonly IFileManager _fileManager;
		public OrderService(CVContext context, IFileManager fileManager)
		{
			_context = context;
			_fileManager = fileManager;
		}
		public async Task<OperationResault> InsertOrderasync(ContactDto contactDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
		{
			try
			{
				string fileName = null;
				if (contactDto.File != null)
				{
					fileName = await _fileManager.SaveProgress(contactDto.File, RootFile.InsertOrderFile, cancellationToken);
				}
				var order = new Contact()
				{
					ContactText = contactDto.ContactText,
					Email = contactDto.Email,
					UserID = Convert.ToInt32(principal.Claims.ToArray()[3].Value),
					Subject = contactDto.Subject,
					FileName = fileName,
				};
				await _context.Contacts.AddAsync(order, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
	}
}
