using System;

namespace Amiq.Services.Group.Contracts.Group
{
    public class DtoGroupEventParticipant
    {
        public Guid GroupEventParticipantId { get; set; }
        public Guid GroupEventId { get; set; }
        public Guid GroupParticipantId { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
