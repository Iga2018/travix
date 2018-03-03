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
	[Route("api/Posts")]
	public class PostsController : Controller
	{
		private readonly IService<PostDTO> _service;
		private readonly IMapper _mapper;

		public PostsController(IService<PostDTO> service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Post>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAsync()
		{
			var postsDto = await _service.GetAsync();
			return Ok(_mapper.Map<IEnumerable<PostDTO>, List<Post>>(postsDto));
		}

		[HttpGet("{id}", Name = "GetPostAsync")]
		[ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetOneAsync(int id)
		{
			var postDto = await _service.GetAsync(id);
			if (postDto == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<PostDTO, Post>(postDto));
		}

		[HttpPost]
		[ValidateEntity]
		[ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> PostAsync([FromBody]Post post)
		{
			try
			{
				var createdPost = await _service.AddAsync(_mapper.Map<Post, PostDTO>(post));
				return CreatedAtAction(nameof(GetOneAsync), new { id = createdPost.Id }, _mapper.Map<PostDTO, Post>(createdPost));
			}
			catch (OperationException)
			{
				return BadRequest();
			}
		}

		[HttpPut]
		[ValidateEntity]
		[ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> PutAsync([FromBody]Post post)
		{
			try
			{
				var updatedPost = await _service.UpdateAsync(_mapper.Map<Post, PostDTO>(post));
				return Ok(_mapper.Map<PostDTO, Post>(updatedPost));
			}
			catch (OperationException)
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var deletedPost = await _service.DeleteAsync(id);
			if (deletedPost == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<PostDTO, Post>(deletedPost));
		}

		protected override void Dispose(bool disposing)
		{
			_service.Dispose();
			base.Dispose(disposing);
		}
	}
}
