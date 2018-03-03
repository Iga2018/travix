using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travix.API.DataAccess.EF;
using Travix.API.DataAccess.Interfaces;

namespace Travix.API.DataAccess.Implementations
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly BlogContext _context;
		private readonly DbSet<T> _entities;

		public Repository(BlogContext context)
		{
			_context = context;
			_entities = _context.Set<T>();
		}

		public async Task<T> CreateAsync(T entity)
		{
			try
			{
				if (entity != null)
				{
					_entities.Add(entity);
				}
				await _context.SaveChangesAsync();
				return entity;
			}
			catch (DbUpdateException)
			{
				return null; //todo: add logging
			}
		}

		public async Task<T> DeleteAsync(int id)
		{
			var entity = await _entities.FindAsync(id);
			if (entity != null)
			{
				_entities.Remove(entity);
			}
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<IEnumerable<T>> ReadAllAsync()
		{
			return await _entities.ToListAsync();
		}

		public async Task<T> ReadOneAsync(int id)
		{
			return await _entities.FindAsync(id);
		}

		public async Task<T> UpdateAsync(T entity)
		{
			try
			{
				if (entity != null)
				{
					_context.Entry(entity).State = EntityState.Modified;
				}
				await _context.SaveChangesAsync();
				return entity;
			}
			catch (DbUpdateException)
			{
				return null; //todo: add logging
			}
		}
	}
}
