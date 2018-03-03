using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travix.API.BusinessLogic.DTO;
using Travix.API.BusinessLogic.Infrustructure;
using Travix.API.BusinessLogic.Services;
using Travix.API.DataAccess.Interfaces;
using Travix.API.DataAccess.Models;

namespace Travix.API.UnitTests.BusinessLogic.Tests
{
	class PostServiceTests
	{
		private PostService _service;
		private Mock<IUnitOfWork> _uowMock;
		private Mock<IMapper> _mapperMock;

		public PostServiceTests()
		{
			_uowMock = new Mock<IUnitOfWork>();
			_mapperMock = new Mock<IMapper>();
			_service = new PostService(_uowMock.Object, _mapperMock.Object);
		}

		[Test(Description = "PostAsync method should throw InvalidOperationException if the result from DAL is null")]
		public void PostAsync_InvalidOperationExceptionTest()
		{
			_uowMock.Setup(x => x.Posts.CreateAsync(It.IsAny<Post>())).ReturnsAsync((Post)null);
			Assert.That(async () => await _service.AddAsync(null), Throws.InstanceOf<OperationException>());
		}

		[Test]
		public async Task PostAsync_CorrectDataTest()
		{
			var postDto = new PostDTO();
			_uowMock.Setup(x => x.Posts.CreateAsync(It.IsAny<Post>())).ReturnsAsync(new Post());
			_mapperMock.Setup(x => x.Map<Post, PostDTO>(It.IsAny<Post>())).Returns(postDto);
			var result = await _service.AddAsync(postDto);
			Assert.AreEqual(result, postDto);
		}

		[Test]
		public async Task DeleteAsync_CorrectDataTest()
		{
			var postDto = new PostDTO();
			_uowMock.Setup(x => x.Posts.DeleteAsync(It.IsAny<int>())).ReturnsAsync(new Post());
			_mapperMock.Setup(x => x.Map<Post, PostDTO>(It.IsAny<Post>())).Returns(postDto);
			var result = await _service.DeleteAsync(It.IsAny<int>());
			Assert.AreEqual(result, postDto);
		}

		[Test]
		public async Task UpdateAsync_CorrectDataTest()
		{
			var postDto = new PostDTO();
			_uowMock.Setup(x => x.Posts.UpdateAsync(It.IsAny<Post>())).ReturnsAsync(new Post());
			_mapperMock.Setup(x => x.Map<Post, PostDTO>(It.IsAny<Post>())).Returns(postDto);
			var result = await _service.UpdateAsync(It.IsAny<PostDTO>());
			Assert.AreEqual(result, postDto);
		}

		[Test(Description = "UpdateAsync method should throw InvalidOperationException if the result from DAL is null")]
		public void UpdateAsync_InvalidOperationExceptionTest()
		{
			_uowMock.Setup(x => x.Posts.UpdateAsync(It.IsAny<Post>())).ReturnsAsync((Post)null);
			Assert.That(async () => await _service.UpdateAsync(It.IsAny<PostDTO>()), Throws.InstanceOf<OperationException>());
		}

		[Test]
		public async Task GetPostAsync_CorrectDataTest()
		{
			var postDto = new PostDTO();
			_uowMock.Setup(x => x.Posts.ReadOneAsync(It.IsAny<int>())).ReturnsAsync(new Post());
			_mapperMock.Setup(x => x.Map<Post, PostDTO>(It.IsAny<Post>())).Returns(postDto);
			var result = await _service.GetAsync(It.IsAny<int>());
			Assert.AreEqual(result, postDto);
		}

		[Test]
		public async Task GetAsync_CorrectDataTest()
		{
			var postsDto = new List<PostDTO>();
			_uowMock.Setup(x => x.Posts.ReadAllAsync()).ReturnsAsync(new List<Post>());
			_mapperMock.Setup(x => x.Map<IEnumerable<Post>, List<PostDTO>>(It.IsAny<IEnumerable<Post>>())).Returns(postsDto);
			var result = await _service.GetAsync();
			CollectionAssert.AreEquivalent(result, postsDto);
		}
	}
}
