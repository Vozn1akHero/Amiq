namespace Amiq.Services.Common.DbOperation
{
    public class PaginatedRequest
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public string SortDirection { get; set; }
    }
}
