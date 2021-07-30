using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models
{
    public partial class GroupPost
    {
        public Guid GroupPostId { get; set; }
        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
