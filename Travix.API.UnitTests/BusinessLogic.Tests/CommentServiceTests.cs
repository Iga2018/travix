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
	class CommentServiceTests
	{
		private CommentService _service;
		private Mock<IUnitOfWork> _uowMock;
		private Mock<IMapper> _mapperMock;

		public CommentServiceTests()
		{
			_uowMock = new Mock<IUnitOfWork>();
			_mapperMock = new Mock<IMapper>();
			_service = new CommentService(_uowMock.Object, _mapperMock.Object);
		}

		[Test(Description = "AddAsync method should throw InvalidOperationException if the result from DAL is null")]
		public void AddAsync_InvalidOperationExceptionTest()
		{
			_uowMock.Setup(x => x.Comments.CreateAsync(It.IsAny<Comment>())).ReturnsAsync((Comment)null);
			Assert.That(async () => await _service.AddAsync(null), Throws.InstanceOf<OperationException>());
		}

		[Test]
		public async Task AddAsync_CorrectDataTest()
		{
			var commentDto = new CommentDTO();
			_uowMock.Setup(x => x.Comments.CreateAsync(It.IsAny<Comment>())).ReturnsAsync(new Comment());
			_mapperMock.Setup(x => x.Map<Comment, CommentDTO>(It.IsAny<Comment>())).Returns(commentDto);
			var result = await _service.AddAsync(commentDto);
			Assert.AreEqual(result, commentDto);
		}

		[Test]
		public async Task DeleteAsync_CorrectDataTest()
		{
			var commentDto = new CommentDTO();
			_uowMock.Setup(x => x.Comments.DeleteAsync(It.IsAny<int>())).ReturnsAsync(new Comment());
			_mapperMock.Setup(x => x.Map<Comment, CommentDTO>(It.IsAny<Comment>())).Returns(commentDto);
			var result = await _service.DeleteAsync(It.IsAny<int>());
			Assert.AreEqual(result, commentDto);
		}

		[Test]
		public async Task UpdateAsync_CorrectDataTest()
		{
			var commentDto = new CommentDTO();
			_uowMock.Setup(x => x.Comments.UpdateAsync(It.IsAny<Comment>())).ReturnsAsync(new Comment());
			_mapperMock.Setup(x => x.Map<Comment, CommentDTO>(It.IsAny<Comment>())).Returns(commentDto);
			var result = await _service.UpdateAsync(It.IsAny<CommentDTO>());
			Assert.AreEqual(result, commentDto);
		}

		[Test(Description = "UpdateAsync method should throw InvalidOperationException if the result from DAL is null")]
		public void UpdateAsync_InvalidOperationExceptionTest()
		{
			_uowMock.Setup(x => x.Comments.UpdateAsync(It.IsAny<Comment>())).ReturnsAsync((Comment)null);
			Assert.That(async () => await _service.UpdateAsync(It.IsAny<CommentDTO>()), Throws.InstanceOf<OperationException>());
		}

		[Test]
		public async Task GetCommentAsync_CorrectDataTest()
		{
			var commentDto = new CommentDTO();
			_uowMock.Setup(x => x.Comments.ReadOneAsync(It.IsAny<int>())).ReturnsAsync(new Comment());
			_mapperMock.Setup(x => x.Map<Comment, CommentDTO>(It.IsAny<Comment>())).Returns(commentDto);
			var result = await _service.GetAsync(It.IsAny<int>());
			Assert.AreEqual(result, commentDto);
		}

		[Test]
		public async Task GetAsync_CorrectDataTest()
		{
			var commentsDto = new List<CommentDTO>();
			_uowMock.Setup(x => x.Comments.ReadAllAsync()).ReturnsAsync(new List<Comment>());
			_mapperMock.Setup(x => x.Map<IEnumerable<Comment>, List<CommentDTO>>(It.IsAny<IEnumerable<Comment>>())).Returns(commentsDto);
			var result = await _service.GetAsync();
			CollectionAssert.AreEquivalent(result, commentsDto);
		}
	}
}
