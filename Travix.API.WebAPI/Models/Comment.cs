using System.ComponentModel.DataAnnotations;

namespace Travix.API.WebAPI.Models
{
	public class Comment
	{
		public int Id { get; set; }
		[Required]
		public string Text { get; set; }
		public string Author { get; set; }
		[Required]
		public int PostId { get; set; }
		public Post Post { get; set; }
	}
}
