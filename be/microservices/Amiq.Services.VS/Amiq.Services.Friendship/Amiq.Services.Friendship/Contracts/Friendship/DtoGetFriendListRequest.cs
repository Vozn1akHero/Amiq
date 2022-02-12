using Amiq.Services.Common.Contracts;

namespace Amiq.Services.Friendship.Contracts.Friendship
{
    public class DtoGetFriendListRequest : DtoPaginatedRequest
    {
        public int IssuerId { get; set; }
    }
}
