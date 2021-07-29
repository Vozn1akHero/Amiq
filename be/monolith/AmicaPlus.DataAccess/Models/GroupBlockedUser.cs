using System;
using System.Collections.Generic;

#nullable disable

namespace temp.Models
{
    public partial class GroupBlockedUser
    {
        public Guid GroupBlockedUserId { get; set; }
        public int GroupId { get; set; }
        public string UserInt { get; set; }
        public DateTime BanDate { get; set; }
    }
}
