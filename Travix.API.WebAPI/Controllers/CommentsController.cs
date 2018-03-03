using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travix.API.BusinessLogic.DTO;
using Travix.API.BusinessLogic.Infrustructure;
using Travix.API.BusinessLogic.Interfaces;
using Travix.API.WebAPI.Filters;
using Travix.API.WebAPI.Models;

namespace Travix.API.WebAPI.Controllers
{
	[Produces("application/json")]
	[Route("api/Comments")]
	public class CommentsController : Controller
	{
		private readonly IService<CommentDTO> _service;
		private readonly IMapper _mapper;

		public CommentsController(IService<CommentDTO> service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Comment>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAsync()
		{
			var commentsDto = await _service.GetAsync();
			return Ok(_mapper.Map<IEnumerable<CommentDTO>, List<Comment>>(commentsDto));
		}

		[HttpGet("{id}", Name = "GetCommentAsync")]
		[ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetOneAsync(int id)
		{
			var commentDto = await _service.GetAsync(id);
			if (commentDto == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<CommentDTO, Comment>(commentDto));
		}

		[HttpPost]
		[ValidateEntity]
		[ProducesResponseType(typeof(Comment), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> PostAsync([FromBody]Comment comment)
		{
			try
			{
				var createdComment = await _service.AddAsync(_mapper.Map<Comment, CommentDTO>(comment));
				return CreatedAtAction(nameof(GetOneAsync),
					new { id = createdComment.Id },
					_mapper.Map<CommentDTO, Comment>(createdComment));
			}
			catch (OperationException)
			{
				return BadRequest();
			}
		}

		[HttpPut]
		[ValidateEntity]
		[ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> PutAsync([FromBody]Comment comment)
		{
			try
			{
				var updatedComment = await _service.UpdateAsync(_mapper.Map<Comment, CommentDTO>(comment));
				return Ok(_mapper.Map<CommentDTO, Comment>(updatedComment));
			}
			catch (OperationException)
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var deletedComment = await _service.DeleteAsync(id);
			if (deletedComment == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<CommentDTO, Comment>(deletedComment));
		}

		protected override void Dispose(bool disposing)
		{
			_service.Dispose();
			base.Dispose(disposing);
		}
	}
}
