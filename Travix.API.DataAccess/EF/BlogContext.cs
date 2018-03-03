using Microsoft.EntityFrameworkCore;
using Travix.API.DataAccess.Models;

namespace Travix.API.DataAccess.EF
{
	public class BlogContext : DbContext
	{
		public BlogContext(DbContextOptions<BlogContext> options)
			: base(options)
		{

		}
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}
