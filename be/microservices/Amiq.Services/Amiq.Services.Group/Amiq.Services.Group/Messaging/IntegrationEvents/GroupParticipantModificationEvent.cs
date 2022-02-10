using Amiq.Services.Friendship.Amqp.IntegrationEvents;

namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public class GroupParticipantModificationEvent : IntegrationEvent
    {
        public Guid GroupParticipantId { get; private set; }
        public int UserId { get; private set; }
        public int GroupId { get; private set; }
        public string Action { get; private set; }

        public GroupParticipantModificationEvent(Guid groupParticipantId, int userId, int groupId, string action)
        {
            GroupParticipantId = groupParticipantId;
            UserId = userId;
            GroupId = groupId;
            Action = action;
        }

    }
}
