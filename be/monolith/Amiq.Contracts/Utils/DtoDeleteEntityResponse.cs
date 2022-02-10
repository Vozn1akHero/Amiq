namespace Amiq.Contracts.Utils
{
    public class DtoDeleteEntityResponse
    {
        public bool IsBusinessException { get; set; }
        public string BusinessException { get; set; }
        public object Entity { get; set; }
    }
}
