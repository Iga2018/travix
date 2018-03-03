using System.Collections.Generic;

namespace Travix.API.DataAccess.Models
{
	public class Post  
	{
		public Post()
		{
			Comments = new List<Comment>();
		}

		public int Id { get; set; }
		public string Text { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
	}
}
