namespace Amiq.Contracts
{
    public class DtoCreateEntityResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
