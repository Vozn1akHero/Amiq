using System;
using System.Collections.Generic;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class BlockedUser
    {
        public Guid BlockedUsersId { get; set; }
        public int IssuerId { get; set; }
        public int DestUserId { get; set; }

        public virtual User DestUser { get; set; }
        public virtual User Issuer { get; set; }
    }
}
