using Amiq.Services.Friendship.Messaging.IntegrationEvents;

namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public class FriendshipModificationEvent : IntegrationEvent
    {
        public Guid FriendshipId { get; private set; }
        public int FirstUserId { get; private set; }
        public int SecondUserId { get; private set; }
        public string Action { get; private set; }

        public FriendshipModificationEvent(Guid friendshipId, int firstUserId, int secondUserId, string action)
        {
            FriendshipId = friendshipId;
            FirstUserId = firstUserId;
            SecondUserId = secondUserId;
            Action = action;
        }
    }
}
