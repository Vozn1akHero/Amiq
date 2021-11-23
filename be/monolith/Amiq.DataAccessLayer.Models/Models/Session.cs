using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class Session
    {
        public Guid SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string SessionToken { get; set; }

        public virtual User User { get; set; }
    }
}
