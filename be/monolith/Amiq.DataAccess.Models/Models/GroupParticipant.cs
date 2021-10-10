﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class GroupParticipant
    {
        public Guid GroupParticipantId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime Joined { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}