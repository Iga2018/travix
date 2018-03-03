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
	public class CommentService : IService<CommentDTO>
	{
		private readonly IUnitOfWork _database;
		private readonly IMapper _mapper;

		public CommentService(IUnitOfWork database, IMapper mapper)
		{
			_database = database;
			_mapper = mapper;
		}

		public async Task<CommentDTO> AddAsync(CommentDTO commentDto)
		{
			var createdComment = await _database.Comments.CreateAsync(_mapper.Map<CommentDTO, Comment>(commentDto));
			if (createdComment == null)
			{
				throw new OperationException("cannot add entity to database");
			}
			return _mapper.Map<Comment, CommentDTO>(createdComment);
		}

		public async Task<CommentDTO> DeleteAsync(int commentId)
		{
			var deletedComment = await _database.Comments.DeleteAsync(commentId);
			return _mapper.Map<Comment, CommentDTO>(deletedComment);
		}

		public async Task<CommentDTO> GetAsync(int commentId)
		{
			var comment = await _database.Comments.ReadOneAsync(commentId);
			return _mapper.Map<Comment, CommentDTO>(comment);
		}

		public async Task<IEnumerable<CommentDTO>> GetAsync()
		{
			var comments = await _database.Comments.ReadAllAsync();
			return _mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(comments);
		}

		public async Task<CommentDTO> UpdateAsync(CommentDTO commentDto)
		{
			var updatedComment = await _database.Comments.UpdateAsync(_mapper.Map<CommentDTO, Comment>(commentDto));
			if (updatedComment == null)
			{
				throw new OperationException("cannot update this entity");
			}
			return _mapper.Map<Comment, CommentDTO>(updatedComment);
		}

		public void Dispose()
		{
			_database.Dispose();
		}
	}
}
