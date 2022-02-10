namespace Amiq.Services.Friendship.Amqp.IntegrationEvents
{
    public class FriendshipRequestAccepted : IntegrationEvent
    {
        public int IssuerId { get; private set; }
        public int ReceiverId { get; private set; }

        public FriendshipRequestAccepted(int issuerId, int receiverId)
        {
            IssuerId = issuerId;
            ReceiverId = receiverId;
        }
    }
}
