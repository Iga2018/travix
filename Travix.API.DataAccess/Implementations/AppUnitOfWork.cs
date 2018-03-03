using System;
using Travix.API.DataAccess.EF;
using Travix.API.DataAccess.Interfaces;
using Travix.API.DataAccess.Models;

namespace Travix.API.DataAccess.Implementations
{
	public class AppUnitOfWork : IUnitOfWork 
	{
		private readonly BlogContext _dbContext;
		private Repository<Post> _postRepository;
		private Repository<Comment> _commentRepository;

		public AppUnitOfWork(BlogContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IRepository<Post> Posts
		{
			get
			{
				if (_postRepository == null)
				{
					_postRepository = new Repository<Post>(_dbContext);
				}
				return _postRepository;
			}
		}

		public IRepository<Comment> Comments
		{
			get
			{
				if (_commentRepository == null)
				{
					_commentRepository = new Repository<Comment>(_dbContext);
				}
				return _commentRepository;
			}
		}

		#region dispose
		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
