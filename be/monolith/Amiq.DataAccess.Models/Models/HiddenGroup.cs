using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class HiddenGroup
    {
        public Guid HiddenGroupId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
