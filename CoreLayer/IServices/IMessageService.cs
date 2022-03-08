using CoreLayer.Utilities;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IMessageService
	{
		Task<List<Contact>> GetAll();
		Task<Contact> Get(int Id);
		Task<OperationResault> Delete(int id);
	}
}
