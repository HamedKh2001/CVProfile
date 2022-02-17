using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly CVContext context;
		private DbSet<T> entities;

		public GenericRepository(CVContext context)
		{
			this.context = context;
			entities = context.Set<T>();
		}

		public IQueryable<T> GetAll()
		{
			return entities.AsQueryable();
		}

		public async Task<T> Getasync(int id)
		{
			return await entities.FindAsync(id);
		}

		public IQueryable<T> GetQueryable(int id)
		{
			return entities.Where(x => x.Id == id).AsQueryable();
		}

		public async Task<OperationResault> Insertasync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			try
			{
				await entities.AddAsync(entity);
				await SaveChangeasync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{

				return OperationResault.Error(ex.Message);
			}
		}

		public OperationResault Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			try
			{
				entities.Update(entity);
				context.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}


		}

		public OperationResault Delete(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			try
			{
				entities.Remove(entity);
				context.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}

		}
		private async Task SaveChangeasync()
		{
			await context.SaveChangesAsync();
		}
	}

}
