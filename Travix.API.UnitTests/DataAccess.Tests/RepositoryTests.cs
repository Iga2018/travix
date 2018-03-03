using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Travix.API.DataAccess.EF;
using Travix.API.DataAccess.Implementations;

namespace Travix.API.UnitTests.DataAccess.Tests
{
	class RepositoryTests<T> where T : class
	{
		private Repository<T> _repository;
		private Mock<BlogContext> _context;

		public RepositoryTests()
		{
			_context = new Mock<BlogContext>();
			_repository = new Repository<T>(_context.Object);
		}

		[Test]
		public async Task CreateAsync_NullArgumentTest()
		{
			var result = await _repository.CreateAsync(null);
			Assert.IsNull(result);
		}
	}
}
