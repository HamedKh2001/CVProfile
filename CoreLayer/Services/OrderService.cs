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
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class OrderService : IOrderService
	{
		private readonly IFileManager _fileManager;
		private readonly CVContext _context;
		public OrderService(IFileManager fileManager, CVContext context)
		{
			_context = context;
			_fileManager = fileManager;
		}
		public OperationResault InsertOrder(ContactDto contactDto,ClaimsPrincipal principal)
		{
			try
			{
				var x = Convert.ToInt32(principal.Claims.ToArray()[3].Value);
				var filename = _fileManager.SaveFile(contactDto.File, RootFile.InsertOrderFile);
				var order = new Contact()
				{
					ContactText = contactDto.ContactText,
					Email = contactDto.Email,
					UserID = Convert.ToInt32(principal.Claims.ToArray()[3].Value),
					Subject = contactDto.Subject,
					FileName = filename
				};
				_context.Contacts.Add(order);
				_context.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
	}
}
