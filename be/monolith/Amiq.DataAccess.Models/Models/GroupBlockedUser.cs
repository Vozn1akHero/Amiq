using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class GroupBlockedUser
    {
        public Guid GroupBlockedUserId { get; set; }
        public int GroupId { get; set; }
        public string UserInt { get; set; }
        public DateTime BanDate { get; set; }
    }
}
