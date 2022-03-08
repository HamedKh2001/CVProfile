using CoreLayer.IServices;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class sapl : Isapl
	{
		private readonly CVContext _cvContext;
		private readonly IDbContextTransaction _transaction;
		public sapl(CVContext cvContext)
		{
			_cvContext = cvContext;
			_transaction= _cvContext.Database.BeginTransaction(); ;
		}
		public List<Skill> GetAll()
		{
			return _cvContext.Skills.ToList();
		}
		public async Task<Skill> insert(Skill skill)
		{
			var res =await _cvContext.Skills.AddAsync(skill);
			//var res = _cvContext.Skills.Add(skill);
			var res2 = res.Entity;
			var x = _cvContext.SaveChanges();
			_transaction.Commit();

			//_transaction.Rollback();
			return res2;
		}
	}
}
