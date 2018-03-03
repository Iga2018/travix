using System;
using Travix.API.DataAccess.Models;

namespace Travix.API.DataAccess.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<Post> Posts { get; }
		IRepository<Comment> Comments { get; }
	}
}
