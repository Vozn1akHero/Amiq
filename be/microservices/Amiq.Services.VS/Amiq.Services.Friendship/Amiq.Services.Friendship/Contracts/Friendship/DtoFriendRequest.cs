using Amiq.Services.Friendship.Contracts.User;

namespace Amiq.Services.Friendship.Contracts.Friendship
{
    public class DtoFriendRequest
    {
        public Guid FriendRequestId { get; set; }
        public DtoBasicUserInfo Creator { get; set; }
        public DtoBasicUserInfo Receiver { get; set; }

        public int IssuerId { get; set; }
        public int ReceiverId { get; set; }
    }
}
