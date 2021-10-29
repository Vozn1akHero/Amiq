using Amiq.Common.DbOperation;
using Amiq.Contracts.Utils;

namespace Amiq.Contracts.Friendship
{
    public class DtoGetFriendListRequest : DtoPaginatedRequest
    { 
        public int IssuerId { get; set; }
    }
}
