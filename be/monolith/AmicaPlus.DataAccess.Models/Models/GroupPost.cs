using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models.Models
{
    public partial class GroupPost
    {
        public Guid GroupPostId { get; set; }
        public Guid PostId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Post Post { get; set; }
    }
}
