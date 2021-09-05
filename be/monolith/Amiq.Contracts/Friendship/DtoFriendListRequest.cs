using Amiq.Common.DbOperation;

namespace Amiq.Contracts.Friendship
{
    public class DtoFriendListRequest : PaginatedRequest
    { 
        public int IssuerId { get; set; }
    }
}
