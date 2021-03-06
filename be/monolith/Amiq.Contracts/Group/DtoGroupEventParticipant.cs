using System;

namespace Amiq.Contracts.Group
{
    public class DtoGroupEventParticipant
    {
        public Guid GroupEventParticipantId { get; set; }
        public Guid GroupEventId { get; set; }
        public Guid GroupParticipantId { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
