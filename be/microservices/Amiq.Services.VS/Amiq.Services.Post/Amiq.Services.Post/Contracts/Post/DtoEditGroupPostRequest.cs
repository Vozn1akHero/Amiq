namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoEditGroupPostRequest
    {
        public Guid GroupPostId { get; set; }
        public int EditedBy { get; set; }
        public string Text { get; set; }
    }
}
