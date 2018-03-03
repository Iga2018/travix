using System.Collections.Generic;
using System.Threading.Tasks;

namespace Travix.API.DataAccess.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> ReadAllAsync();
		Task<T> ReadOneAsync(int id);
		Task<T> CreateAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(int id);
	}
}
