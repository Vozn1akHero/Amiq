using System;
using System.Collections.Generic;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class GroupEventParticipant
    {
        public Guid GroupEventParticipantId { get; set; }
        public Guid GroupEventId { get; set; }
        public Guid GroupParticipantId { get; set; }
        public DateTime JoinedAt { get; set; }

        public virtual GroupEvent GroupEvent { get; set; }
        public virtual GroupParticipant GroupParticipant { get; set; }
    }
}
