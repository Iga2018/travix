using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travix.API.WebAPI.Models
{
	public class Post
	{
		public Post()
		{
			Comments = new List<Comment>();
		}

		public int Id { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public string Description { get; set; }
		public string Author { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}
