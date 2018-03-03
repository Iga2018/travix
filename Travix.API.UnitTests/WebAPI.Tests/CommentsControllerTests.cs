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
	class CommentsControllerTests
	{
		private CommentsController _controller;
		private Mock<IService<CommentDTO>> _serviceMock;
		private Mock<IMapper> _mapperMock;

		public CommentsControllerTests()
		{
			_serviceMock = new Mock<IService<CommentDTO>>();
			_mapperMock = new Mock<IMapper>();
			_controller = new CommentsController(_serviceMock.Object, _mapperMock.Object);
		}

		[Test]
		public async Task GetAsync_ReturnsCorrectResultTest()
		{
			var comments = new List<Comment>();
			_serviceMock.Setup(x => x.GetAsync()).ReturnsAsync(new List<CommentDTO>());
			_mapperMock.Setup(x => x.Map<IEnumerable<CommentDTO>, List<Comment>>(It.IsAny<IEnumerable<CommentDTO>>())).Returns(comments);
			var actionResult = await _controller.GetAsync();
			var okResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.IsNotNull(okResult.Value);
			CollectionAssert.AreEquivalent(okResult.Value as IEnumerable<Comment>, comments);
		}

		[Test]
		public async Task GetOneAsync_ReturnsCorrectResultTest()
		{
			var comment = new Comment();
			_serviceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new CommentDTO());
			_mapperMock.Setup(x => x.Map<CommentDTO, Comment>(It.IsAny<CommentDTO>())).Returns(comment);
			var actionResult = await _controller.GetOneAsync(1);
			var okResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.IsNotNull(okResult.Value);
			Assert.AreEqual(okResult.Value as Comment, comment);
		}

		[Test]
		public async Task GetOneAsync_ReturnsNotFoundTest()
		{
			_serviceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((CommentDTO)null);
			var actionResult = await _controller.GetOneAsync(1);
			var notFoundResult = actionResult as NotFoundResult;
			Assert.IsNotNull(notFoundResult);
		}

		[Test]
		public async Task PostAsync_ShouldReturnCreatedEntityTest()
		{
			var comment = new Comment();
			_serviceMock.Setup(x => x.AddAsync(It.IsAny<CommentDTO>())).ReturnsAsync(new CommentDTO());
			_mapperMock.Setup(x => x.Map<CommentDTO, Comment>(It.IsAny<CommentDTO>())).Returns(comment);
			var actionResult = await _controller.PostAsync(It.IsAny<Comment>());
			var createdOkResult = actionResult as CreatedAtActionResult;
			Assert.IsNotNull(createdOkResult);
			Assert.IsNotNull(createdOkResult.Value);
			Assert.AreEqual(createdOkResult.Value as Comment, comment);
		}

		[Test]
		public async Task PostAsync_ReturnsBadRequestTest()
		{
			_serviceMock.Setup(x => x.AddAsync(It.IsAny<CommentDTO>())).ThrowsAsync(new OperationException(It.IsAny<string>()));
			var actionResult = await _controller.PostAsync(It.IsAny<Comment>());
			var badRequestResult = actionResult as BadRequestResult;
			Assert.IsNotNull(badRequestResult);
		}

		[Test]
		public async Task PutAsync_ShouldReturnUpdatedEntityTest()
		{
			var comment = new Comment();
			_serviceMock.Setup(x => x.UpdateAsync(It.IsAny<CommentDTO>())).ReturnsAsync(new CommentDTO());
			_mapperMock.Setup(x => x.Map<CommentDTO, Comment>(It.IsAny<CommentDTO>())).Returns(comment);
			var actionResult = await _controller.PutAsync(It.IsAny<Comment>());
			var updatedOkResult = actionResult as OkObjectResult;
			Assert.IsNotNull(updatedOkResult);
			Assert.IsNotNull(updatedOkResult.Value);
			Assert.AreEqual(updatedOkResult.Value as Comment, comment);
		}

		[Test]
		public async Task PutAsync_ReturnsBadRequestTest()
		{
			_serviceMock.Setup(x => x.UpdateAsync(It.IsAny<CommentDTO>())).ThrowsAsync(new OperationException(It.IsAny<string>()));
			var actionResult = await _controller.PutAsync(It.IsAny<Comment>());
			var badRequestResult = actionResult as BadRequestResult;
			Assert.IsNotNull(badRequestResult);
		}

		[Test]
		public async Task DeleteAsync_ShouldReturnDeletedEntityTest()
		{
			var comment = new Comment();
			_serviceMock.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(new CommentDTO());
			_mapperMock.Setup(x => x.Map<CommentDTO, Comment>(It.IsAny<CommentDTO>())).Returns(comment);
			var actionResult = await _controller.DeleteAsync(It.IsAny<int>());
			var deletedOkResult = actionResult as OkObjectResult;
			Assert.IsNotNull(deletedOkResult);
			Assert.IsNotNull(deletedOkResult.Value);
			Assert.AreEqual(deletedOkResult.Value as Comment, comment);
		}

		[Test]
		public async Task DeleteAsync_ReturnsNotFoundTest()
		{
			_serviceMock.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync((CommentDTO)null);
			var actionResult = await _controller.DeleteAsync(It.IsAny<int>());
			var notFoundResult = actionResult as NotFoundResult;
			Assert.IsNotNull(notFoundResult);
		}
	}
}
