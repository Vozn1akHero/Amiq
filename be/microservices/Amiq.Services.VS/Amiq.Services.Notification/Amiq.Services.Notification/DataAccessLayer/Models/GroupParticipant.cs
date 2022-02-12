using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models
{
    public partial class GroupParticipant
    {
        public Guid GroupParticipantId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime Joined { get; set; }
    }
}
