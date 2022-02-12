namespace Amiq.Services.Friendship.Contracts.Friendship
{
    public class DtoFriendshipStatus
    {
        public bool IssuerReceivedFriendRequest { get; set; }
        public bool IssuerSentFriendRequest { get; set; }
        public bool IsIssuerFriend { get; set; }
    }
}
