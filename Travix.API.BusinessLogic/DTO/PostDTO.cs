using System.Collections.Generic;

namespace Travix.API.BusinessLogic.DTO
{
	public class PostDTO
	{
		public PostDTO()
		{
			Comments = new List<CommentDTO>();
		}

		public int Id { get; set; }
		public string Text { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public ICollection<CommentDTO> Comments { get; set; }
	}
}
