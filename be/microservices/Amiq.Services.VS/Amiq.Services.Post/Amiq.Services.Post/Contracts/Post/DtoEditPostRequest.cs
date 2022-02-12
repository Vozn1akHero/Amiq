namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoEditPostRequest
    {
        public Guid PostId { get; set; }
        public int EditedBy { get; set; }
        public string Text { get; set; }
    }
}
