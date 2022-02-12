using System;
using System.Collections.Generic;

namespace Amiq.Services.User.DataAccessLayer.Models
{
    public partial class BlockedUser
    {
        public Guid BlockedUsersId { get; set; }
        public int IssuerId { get; set; }
        public int DestUserId { get; set; }

        public virtual User DestUser { get; set; } = null!;
        public virtual User Issuer { get; set; } = null!;
    }
}
