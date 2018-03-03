namespace Travix.API.BusinessLogic.DTO
{
	public class CommentDTO
    {
		public int Id { get; set; }
		public string Text { get; set; }
		public string Author { get; set; }
		public int PostId { get; set; }
		public PostDTO Post { get; set; }
	}
}
