using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.DataAccessLayer.Models.Models
{
    public partial class GroupPost
    {
        public Guid GroupPostId { get; set; }
        public Guid PostId { get; set; }
        public int GroupId { get; set; }
        public int AuthorId { get; set; }
        public bool? VisibleAsCreatedByAdmin { get; set; }

        public virtual User Author { get; set; } = null!;
        public virtual Group Group { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
