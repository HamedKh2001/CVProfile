using CoreLayer.IServices;
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
		public MessageService(CVContext cvContext)
		{
			_cvContext = cvContext;
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
	}
}
