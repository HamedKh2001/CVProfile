using CoreLayer.IServices;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class MessageService : IMessageService
	{
		private readonly CVContext _cvContext;
		private readonly IFileManager _fileManager;
		public MessageService(CVContext cvContext, IFileManager fileManager)
		{
			_cvContext = cvContext;
			_fileManager = fileManager;
		}
		public async Task<List<Contact>> GetAll()
		{
			return await _cvContext.Contacts.Include(u => u.User).ToListAsync();
		}
		
		public async Task<Contact> Get(int Id)
		{
			var x = await _cvContext.Contacts.Include(u => u.User).FirstOrDefaultAsync(c => c.Id == Id);
			return x;
		}
		
		public async Task<OperationResault> Delete(int id)
		{
            try
            {
				var message =await _cvContext.Contacts.FindAsync(id);
				_cvContext.Contacts.Remove(message);
				_cvContext.SaveChanges();
				var res = _fileManager.DeleteFile(RootFile.InsertOrderFile, message.FileName);
				return await Task.FromResult(res);
			}
            catch (System.Exception ex)
            {
				return await Task.FromResult(OperationResault.Error(ex.Message));
            }
		}
	}
}
