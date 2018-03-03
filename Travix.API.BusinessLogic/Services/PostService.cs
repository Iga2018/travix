using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travix.API.BusinessLogic.DTO;
using Travix.API.BusinessLogic.Infrustructure;
using Travix.API.BusinessLogic.Interfaces;
using Travix.API.DataAccess.Interfaces;
using Travix.API.DataAccess.Models;

namespace Travix.API.BusinessLogic.Services
{
	public class PostService : IService<PostDTO>
	{
		private readonly IUnitOfWork _database;
		private readonly IMapper _mapper;

		public PostService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_database = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PostDTO> AddAsync(PostDTO postDto)
		{
			var createdPost = await _database.Posts.CreateAsync(_mapper.Map<PostDTO, Post>(postDto));
			if (createdPost == null)
			{
				throw new OperationException("cannot add entity to database");
			}
			return _mapper.Map<Post, PostDTO>(createdPost);
		}

		public async Task<PostDTO> DeleteAsync(int postId)
		{
			var deletedPost = await _database.Posts.DeleteAsync(postId);
			return _mapper.Map<Post, PostDTO>(deletedPost);
		}

		public async Task<PostDTO> GetAsync(int postId)
		{
			var post = await _database.Posts.ReadOneAsync(postId);
			return _mapper.Map<Post, PostDTO>(post);
		}

		public async Task<IEnumerable<PostDTO>> GetAsync()
		{
			var posts = await _database.Posts.ReadAllAsync();
			return _mapper.Map<IEnumerable<Post>, List<PostDTO>>(posts);
		}

		public async Task<PostDTO> UpdateAsync(PostDTO postDto)
		{
			var updatedPost = await _database.Posts.UpdateAsync(_mapper.Map<PostDTO, Post>(postDto));
			if (updatedPost == null)
			{
				throw new OperationException("cannot update this entity");
			}
			return _mapper.Map<Post, PostDTO>(updatedPost);
		}

		public void Dispose()
		{
			_database.Dispose();
		}
	}
}
