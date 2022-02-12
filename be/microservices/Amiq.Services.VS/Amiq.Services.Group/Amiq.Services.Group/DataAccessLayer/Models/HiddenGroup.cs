using System;
using System.Collections.Generic;

namespace Amiq.Services.Group.DataAccessLayer.Models
{
    public partial class HiddenGroup
    {
        public Guid HiddenGroupId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
