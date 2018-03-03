using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travix.API.BusinessLogic.DTO;
using Travix.API.BusinessLogic.Infrustructure;
using Travix.API.BusinessLogic.Interfaces;
using Travix.API.WebAPI.Controllers;
using Travix.API.WebAPI.Models;

namespace Travix.API.UnitTests.WebAPI.Tests
{
	class PostsControllerTests
	{
		private PostsController _controller;
		private Mock<IService<PostDTO>> _serviceMock;
		private Mock<IMapper> _mapperMock;

		public PostsControllerTests()
		{
			_serviceMock = new Mock<IService<PostDTO>>();
			_mapperMock = new Mock<IMapper>();
			_controller = new PostsController(_serviceMock.Object, _mapperMock.Object);
		}

		[Test]
		public async Task GetAsync_ReturnsCorrectResultTest()
		{
			var posts = new List<Post>();
			_serviceMock.Setup(x => x.GetAsync()).ReturnsAsync(new List<PostDTO>());
			_mapperMock.Setup(x => x.Map<IEnumerable<PostDTO>, List<Post>>(It.IsAny<IEnumerable<PostDTO>>())).Returns(posts);
			var actionResult = await _controller.GetAsync();
			var okResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.IsNotNull(okResult.Value);
			CollectionAssert.AreEquivalent(okResult.Value as IEnumerable<Post>, posts);
		}

		[Test]
		public async Task GetOneAsync_ReturnsCorrectResultTest()
		{
			var post = new Post();
			_serviceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new PostDTO());
			_mapperMock.Setup(x => x.Map<PostDTO, Post>(It.IsAny<PostDTO>())).Returns(post);
			var actionResult = await _controller.GetOneAsync(1);
			var okResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.IsNotNull(okResult.Value);
			Assert.AreEqual(okResult.Value as Post, post);
		}

		[Test]
		public async Task GetOneAsync_ReturnsNotFoundTest()
		{
			_serviceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((PostDTO)null);
			var actionResult = await _controller.GetOneAsync(1);
			var notFoundResult = actionResult as NotFoundResult;
			Assert.IsNotNull(notFoundResult);
		}

		[Test]
		public async Task PostAsync_ShouldReturnCreatedEntityTest()
		{
			var post = new Post();
			_serviceMock.Setup(x => x.AddAsync(It.IsAny<PostDTO>())).ReturnsAsync(new PostDTO());
			_mapperMock.Setup(x => x.Map<PostDTO, Post>(It.IsAny<PostDTO>())).Returns(post);
			var actionResult = await _controller.PostAsync(It.IsAny<Post>());
			var createdOkResult = actionResult as CreatedAtActionResult;
			Assert.IsNotNull(createdOkResult);
			Assert.IsNotNull(createdOkResult.Value);
			Assert.AreEqual(createdOkResult.Value as Post, post);
		}

		[Test]
		public async Task PostAsync_ReturnsBadRequestTest()
		{
			_serviceMock.Setup(x => x.AddAsync(It.IsAny<PostDTO>())).ThrowsAsync(new OperationException(It.IsAny<string>()));
			var actionResult = await _controller.PostAsync(It.IsAny<Post>());
			var badRequestResult = actionResult as BadRequestResult;
			Assert.IsNotNull(badRequestResult);
		}

		[Test]
		public async Task PutAsync_ShouldReturnUpdatedEntityTest()
		{
			var post = new Post();
			_serviceMock.Setup(x => x.UpdateAsync(It.IsAny<PostDTO>())).ReturnsAsync(new PostDTO());
			_mapperMock.Setup(x => x.Map<PostDTO, Post>(It.IsAny<PostDTO>())).Returns(post);
			var actionResult = await _controller.PutAsync(It.IsAny<Post>());
			var updatedOkResult = actionResult as OkObjectResult;
			Assert.IsNotNull(updatedOkResult);
			Assert.IsNotNull(updatedOkResult.Value);
			Assert.AreEqual(updatedOkResult.Value as Post, post);
		}

		[Test]
		public async Task PutAsync_ReturnsBadRequestTest()
		{
			_serviceMock.Setup(x => x.UpdateAsync(It.IsAny<PostDTO>())).ThrowsAsync(new OperationException(It.IsAny<string>()));
			var actionResult = await _controller.PutAsync(It.IsAny<Post>());
			var badRequestResult = actionResult as BadRequestResult;
			Assert.IsNotNull(badRequestResult);
		}

		[Test]
		public async Task DeleteAsync_ShouldReturnDeletedEntityTest()
		{
			var post = new Post();
			_serviceMock.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(new PostDTO());
			_mapperMock.Setup(x => x.Map<PostDTO, Post>(It.IsAny<PostDTO>())).Returns(post);
			var actionResult = await _controller.DeleteAsync(It.IsAny<int>());
			var deletedOkResult = actionResult as OkObjectResult;
			Assert.IsNotNull(deletedOkResult);
			Assert.IsNotNull(deletedOkResult.Value);
			Assert.AreEqual(deletedOkResult.Value as Post, post);
		}

		[Test]
		public async Task DeleteAsync_ReturnsNotFoundTest()
		{
			_serviceMock.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync((PostDTO)null);
			var actionResult = await _controller.DeleteAsync(It.IsAny<int>());
			var notFoundResult = actionResult as NotFoundResult;
			Assert.IsNotNull(notFoundResult);
		}
	}
}
