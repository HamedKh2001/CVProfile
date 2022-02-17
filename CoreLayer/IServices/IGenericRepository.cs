using CORETest.Utilities;
using DataLayer.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IGenericRepository<T> where T : class
	{
		IQueryable<T> GetAll();
		Task<T> Getasync(int id);
		IQueryable<T> GetQueryable(int id);
		Task<OperationResault> Insertasync(T entity);
		OperationResault Update(T entity);
		OperationResault Delete(T entity);
	}
}
