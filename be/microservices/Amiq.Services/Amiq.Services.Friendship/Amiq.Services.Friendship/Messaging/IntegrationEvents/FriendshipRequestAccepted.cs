namespace Amiq.Services.Friendship.Messaging.IntegrationEvents
{
    public class FriendshipRequestAccepted : IntegrationEvent
    {
        public FriendshipRequestAccepted(Guid friendRequestId, int issuerId, int receiverId)
        {
            FriendRequestId = friendRequestId;
            IssuerId = issuerId;
            ReceiverId = receiverId;
        }

        public Guid FriendRequestId{ get; set; }
        public int IssuerId { get; set; }
        public int ReceiverId { get; set; }


    }
}
