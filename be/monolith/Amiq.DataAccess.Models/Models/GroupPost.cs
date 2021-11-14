using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class GroupPost
    {
        public Guid GroupPostId { get; set; }
        public Guid PostId { get; set; }
        public int GroupId { get; set; }
        public int AuthorId { get; set; }

        public virtual User Author { get; set; }
        public virtual Group Group { get; set; }
        public virtual Post Post { get; set; }
    }
}
