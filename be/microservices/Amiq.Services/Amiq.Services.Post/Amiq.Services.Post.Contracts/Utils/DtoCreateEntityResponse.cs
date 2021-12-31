namespace Amiq.Services.Post.Contracts.Utils
{
    public class DtoCreateEntityResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
