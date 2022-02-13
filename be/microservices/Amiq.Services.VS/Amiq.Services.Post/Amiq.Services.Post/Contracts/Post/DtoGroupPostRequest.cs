using Amiq.Services.Common.Contracts;

namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoGroupPostRequest : DtoPaginatedRequest
    {
        public int GroupId { get; set; }
    }
}
