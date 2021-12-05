using Amiq.Common.DbOperation;
using Amiq.Services.Friendship.Contracts.Utils;

namespace Amiq.Services.Friendship.Contracts.Friendship
{
    public class DtoGetFriendListRequest : DtoPaginatedRequest
    {
        public int IssuerId { get; set; }
    }
}
