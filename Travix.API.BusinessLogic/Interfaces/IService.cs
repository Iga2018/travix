using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Travix.API.BusinessLogic.Interfaces
{
	public interface IService<T> : IDisposable where T : class
	{
		Task<IEnumerable<T>> GetAsync();
		Task<T> GetAsync(int id);
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(int id);
	}
}
